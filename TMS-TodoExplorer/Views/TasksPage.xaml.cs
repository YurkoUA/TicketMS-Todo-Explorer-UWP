using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TMS.TodoExplorer.ViewModels;
using TMS.TodoExplorer.Util;
using TMS.TodoApi.Interfaces;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class TasksPage : Page
    {
        public TasksPage()
        {
            InitializeComponent();
            DataContext = new TasksViewModel(AutofacConfig.Resolve<ITodoService>(),
                                                AutofacConfig.Resolve<INavigationService>());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();
        }
    }
}
