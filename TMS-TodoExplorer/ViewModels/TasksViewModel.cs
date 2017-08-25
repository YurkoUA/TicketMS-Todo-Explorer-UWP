using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using TMS.TodoApi.Exceptions;
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
        private IAuthenticationService _authService;

        private ICommand _openCreatePage;
        private ICommand _openDetailsPage;
        private ICommand _openEditPage;
        private ICommand _confirmDeletionCommand;

        private ICommand _confirmLogoutCommand;
        private ICommand _logoutCommand;

        private TodoTask _selectedTask;
        private int _selectedPivotIndex = 0;

        public TasksViewModel(ITodoService todoService, INavigationService navigService, IAuthenticationService authService)
        {
            _todoService = todoService;
            _navigationService = navigService;
            _authService = authService;

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

        public TodoTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public int SelectedPivotIndex
        {
            get => _selectedPivotIndex;
            set
            {
                _selectedPivotIndex = value;
                OnPropertyChanged("SelectedPivotIndex");
            }
        }

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

        public ICommand OpenDetailsPage
        {
            get
            {
                return _openDetailsPage ?? (_openDetailsPage = new SimpleCommand(obj =>
                {
                    OpenDetails();
                }));
            }
        }

        public ICommand OpenEditPage
        {
            get
            {
                return _openEditPage ?? (_openEditPage = new SimpleCommand(obj =>
                {
                    if (SelectedTask != null)
                    {
                        _navigationService.NavigateTo(typeof(EditPage), SelectedTask.Id);
                    }
                }));
            }
        }

        public ICommand ConfirmDeletion
        {
            get
            {
                return _confirmDeletionCommand ?? (_confirmDeletionCommand = new SimpleCommand(async obj =>
                {
                    var deleteDialog = new ContentDialog
                    {
                        Title = $"Видалення задачі \"{SelectedTask.Title}\"",
                        Content = $"Ви дійсно бажаєте видалити задачу \"{SelectedTask.Title}\"?",
                        CloseButtonText = "Ні",
                        PrimaryButtonText = "Так",

                        PrimaryButtonCommand = new SimpleCommand(async o =>
                        {
                            await DeleteAsync();
                        })
                    };
                    await deleteDialog.ShowAsync();
                }));
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new SimpleCommand(obj =>
                {
                    _authService.Logout();
                    _navigationService.NavigateTo(typeof(SignInPage));
                }));
            }
        }

        public ICommand ConfirmLogoutCommand
        {
            get
            {
                return _confirmLogoutCommand ?? (_confirmLogoutCommand = new SimpleCommand(async obj =>
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Вихід з аккаунту",
                        Content = "Ви дійсно бажаєте вийти зі свого аккаунту?",
                        CloseButtonText = "Ні",
                        PrimaryButtonText = "Так",
                        PrimaryButtonCommand = LogoutCommand
                    };
                    await dialog.ShowAsync();
                }));
            }
        }

        private async Task DeleteAsync()
        {
            try
            {
                await _todoService.DeleteAsync(SelectedTask.Id);
                await MessageBox.ShowAsync("Задачу успішно видалено.");

                GetListByPivotIndex().Remove(SelectedTask);
                SelectedTask = null;
            }
            catch (NotFoundException)
            {
                await MessageBox.ShowAsync("Задачу не знайдено.\nМожливо, її вже хтось видалив.", "Помилка");
                _navigationService.NavigateTo(typeof(TasksPage));
            }
            catch (HttpResponseException ex)
            {
                await MessageBox.ShowAsync($"Сталася помилка (HTTP {ex.StatusCodeNumber}).\nСпробуйте ще раз.", "Помилка");
            }
            catch
            {
                await MessageBox.ShowAsync("Сталася помилка.\nПеревірте підключення до мережі та спробуйте ще раз.", "Помилка");
            }
        }

        public void OpenDetails()
        {
            if (SelectedTask != null)
            {
                _navigationService.NavigateTo(typeof(DetailsPage), SelectedTask.Id);
            }
        }

        public void ResetSelection()
        {
            SelectedTask = null;
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

        private ObservableCollection<TodoTask> GetListByPivotIndex()
        {
            switch (SelectedPivotIndex)
            {
                case 0:
                    return FreeTasks;
                case 1:
                    return InProcessTasks;
                case 2:
                    return RecycledTasks;
                case 3:
                    return CompletedTasks;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
