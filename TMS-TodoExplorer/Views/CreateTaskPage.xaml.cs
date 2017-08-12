using Windows.UI.Xaml.Controls;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.Util;
using TMS.TodoExplorer.ViewModels;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class CreateTaskPage : Page
    {
        public CreateTaskPage()
        {
            InitializeComponent();
            DataContext = new CreateViewModel(AutofacConfig.Resolve<ITodoService>(),
                                                AutofacConfig.Resolve<INavigationService>());
        }
    }
}
