using Windows.UI.Xaml.Controls;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.ViewModels;
using static TMS.TodoExplorer.Util.AutofacConfig;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
            DataContext = new SignInViewModel(Resolve<IAuthenticationService>(),
                                              Resolve<INavigationService>());
        }
    }
}
