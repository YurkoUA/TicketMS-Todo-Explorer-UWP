using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TMS.TodoApi.Exceptions;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.ViewModels;
using static TMS.TodoExplorer.Util.AutofacConfig;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is int selectedId)
                InitializeTask(selectedId).GetAwaiter();
        }

        public async Task InitializeTask(int id)
        {
            var todoService = Resolve<ITodoService>();
            var navigationService = Resolve<INavigationService>();

            try
            {
                var task = await todoService.GetByIdAsync(id);

                DataContext = new DetailsViewModel(todoService, navigationService)
                {
                    SelectedTask = task
                };
            }
            catch (NotFoundException)
            {
                await MessageBox.ShowAsync("Задачу не знайдено", "Помилка");
                navigationService.NavigateTo(typeof(TasksPage));
            }
        }
    }
}
