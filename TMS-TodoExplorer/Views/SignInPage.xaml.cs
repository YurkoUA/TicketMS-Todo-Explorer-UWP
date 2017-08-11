using Windows.UI.Xaml.Controls;
using TMS.TodoExplorer.ViewModels;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
