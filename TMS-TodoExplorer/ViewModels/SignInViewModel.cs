using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.Commands;
using TMS.TodoExplorer.Views;

namespace TMS.TodoExplorer.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private IAuthenticationService _authService;
        private INavigationService _navigationService;

        private string _userName;
        private string _password;
        private bool? _isRemember;

        private ICommand _authorizeCommand;

        public SignInViewModel(IAuthenticationService authService, INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public bool? IsRemember
        {
            get => _isRemember;
            set
            {
                _isRemember = value;
                OnPropertyChanged("IsRemember");
            }
        }

        public ICommand AuthorizeCommand => _authorizeCommand ??
                    (_authorizeCommand = new SimpleCommand(async obj =>
                    {
                        await AuthorizeAsync();
                    }));

        public async Task AuthorizeAsync()
        {
            var result = await _authService.TryAuthorizeAsync(UserName, Password);

            if (result)
            {
                //var message = $"{_authService.AccessToken}\n{_authService.AccessToken.ExpireTime}";
                //await new MessageDialog(message, "Вас успішно авторизовано").ShowAsync();
                _navigationService.NavigateTo(typeof(TasksPage));
            }
            else
            {
                await new MessageDialog("Невірний логін або пароль.\nАбо ж взагалі сервіс недоступний.", "Помилка авторизації").ShowAsync();
            }
        }
    }
}
