using Windows.UI.Xaml.Controls;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.Util;
using TMS.TodoExplorer.ViewModels;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
            DataContext = new SignInViewModel(AutofacConfig.Resolve<IAuthenticationService>(),
                                                AutofacConfig.Resolve<INavigationService>());
        }
    }
}
