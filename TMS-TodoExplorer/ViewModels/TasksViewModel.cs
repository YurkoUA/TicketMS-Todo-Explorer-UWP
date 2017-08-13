using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TMS.TodoApi.Extensions;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;
using TMS.TodoExplorer.Commands;
using TMS.TodoExplorer.Views;
using TaskStatus = TMS.TodoApi.Enums.TaskStatus;

namespace TMS.TodoExplorer.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private ITodoService _todoService;
        private INavigationService _navigationService;

        private ICommand _openCreatePage;

        public TasksViewModel(ITodoService todoService, INavigationService navigService)
        {
            _todoService = todoService;
            _navigationService = navigService;

            FreeTasks = new ObservableCollection<TodoTask>();
            InProcessTasks = new ObservableCollection<TodoTask>();
            RecycledTasks = new ObservableCollection<TodoTask>();
            CompletedTasks = new ObservableCollection<TodoTask>();

            LoadTasksAsync();
        }

        public ObservableCollection<TodoTask> FreeTasks { get; set; }
        public ObservableCollection<TodoTask> InProcessTasks { get; set; }
        public ObservableCollection<TodoTask> RecycledTasks { get; set; }
        public ObservableCollection<TodoTask> CompletedTasks { get; set; }

        public ICommand OpenCreatePage
        {
            get
            {
                return _openCreatePage ?? (_openCreatePage = new SimpleCommand(obj =>
                {
                    _navigationService.NavigateTo(typeof(CreateTaskPage));
                }));
            }
        }

        private async void LoadTasksAsync()
        {
            var dict = await _todoService.GetTaskGroupByStatusAsync();

            if (dict == null || !dict.Any())
                return;

            if (dict.ContainsKey(TaskStatus.None))
            {
                FreeTasks.AddRange(dict[TaskStatus.None]);
            }

            if (dict.ContainsKey(TaskStatus.InProgress))
            {
                InProcessTasks.AddRange(dict[TaskStatus.InProgress]);
            }

            if (dict.ContainsKey(TaskStatus.Recycle))
            {
                RecycledTasks.AddRange(dict[TaskStatus.Recycle]);
            }

            if (dict.ContainsKey(TaskStatus.Completed))
            {
                CompletedTasks.AddRange(dict[TaskStatus.Completed]);
            }
        }
    }
}
