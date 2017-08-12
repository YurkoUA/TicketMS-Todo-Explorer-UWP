using System.Threading.Tasks;
using System.Windows.Input;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.Commands;
using TMS.TodoExplorer.Views;
using TMS.TodoApi.Exceptions;

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
            try
            {
                await _authService.AuthorizeAsync(UserName, Password);
                _navigationService.NavigateTo(typeof(TasksPage));
            }
            catch (BadRequestException)
            {
                await MessageBox.ShowAsync("Невірний логін або пароль", "Помилка");
            }
            catch (HttpResponseException ex)
            {
                await MessageBox.ShowAsync($"Сервіс недоступний. Перевірте підключення до мережі або спробуйте ще раз.\n\nКод помилки: {ex.StatusCodeNumber}",
                    "Помилка");
            }
            catch
            {
                await MessageBox.ShowAsync("Сталася якась помилка.\n\nПеревірте підключення до мережі.", "Помилка");
            }
        }
    }
}
