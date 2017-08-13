using Windows.UI.Xaml.Controls;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.ViewModels;
using static TMS.TodoExplorer.Util.AutofacConfig;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class CreateTaskPage : Page
    {
        public CreateTaskPage()
        {
            InitializeComponent();
            DataContext = new CreateViewModel(Resolve<ITodoService>(),
                                              Resolve<INavigationService>());
        }
    }
}
