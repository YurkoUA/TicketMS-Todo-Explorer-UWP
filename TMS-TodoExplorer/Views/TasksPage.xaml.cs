using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using TMS.TodoExplorer.ViewModels;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;
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

        private void ListBox_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            ViewModel.SelectedTask = (e?.OriginalSource as Rectangle)?.DataContext as TodoTask;
        }
    }
}
