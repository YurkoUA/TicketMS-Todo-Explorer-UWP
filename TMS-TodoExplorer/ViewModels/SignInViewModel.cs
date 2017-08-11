using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.Commands;
using TMS.TodoExplorer.Util;

namespace TMS.TodoExplorer.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private IAuthenticationService _authService;

        private string _userName;
        private string _password;
        private bool? _isRemember;

        protected ICommand _authorizeCommand;

        public SignInViewModel()
        {
            _authService = AutofacConfig.Resolve<IAuthenticationService>();
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
                var message = $"{_authService.AccessToken}\n{_authService.AccessToken.ExpireTime}";
                await new MessageDialog(message, "Вас успішно авторизовано").ShowAsync();
            }
            else
            {
                await new MessageDialog("Невірний логін або пароль.\nАбо ж взагалі сервіс недоступний.", "Помилка авторизації").ShowAsync();
            }
        }
    }
}
