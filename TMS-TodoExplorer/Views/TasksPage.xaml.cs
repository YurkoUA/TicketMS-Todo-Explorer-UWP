using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TMS.TodoExplorer.ViewModels;
using TMS.TodoApi.Interfaces;
using static TMS.TodoExplorer.Util.AutofacConfig;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class TasksPage : Page
    {
        public TasksViewModel ViewModel => DataContext as TasksViewModel;

        public TasksPage()
        {
            InitializeComponent();
            DataContext = new TasksViewModel(Resolve<ITodoService>(),
                                             Resolve<INavigationService>());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();
        }
    }
}
