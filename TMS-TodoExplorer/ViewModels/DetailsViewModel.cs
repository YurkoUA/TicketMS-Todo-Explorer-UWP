using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TMS.TodoApi.Exceptions;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;
using TMS.TodoExplorer.Commands;
using TMS.TodoExplorer.Views;

namespace TMS.TodoExplorer.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        public DetailsViewModel(ITodoService todoServ, INavigationService navigServ)
        {
            _todoService = todoServ;
            _navigationService = navigServ;
        }

        private ITodoService _todoService;
        private INavigationService _navigationService;

        private ICommand _openEditPageCommand;
        private ICommand _deleteCommand;

        private TodoTask _selectedTask;

        public TodoTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public Visibility DescriptionVisibility
        {
            get => string.IsNullOrEmpty(SelectedTask.Description) ? Visibility.Collapsed : Visibility.Visible;
        }

        public ICommand OpenEditPageCommand
        {
            get => _openEditPageCommand ?? (_openEditPageCommand = new SimpleCommand(obj =>
            {
                _navigationService.NavigateTo(typeof(EditPage), SelectedTask.Id);
            }));
        }

        public ICommand DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new SimpleCommand(async obj =>
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

        public async Task DeleteAsync()
        {
            try
            {
                await _todoService.DeleteAsync(SelectedTask.Id);

                await MessageBox.ShowAsync("Задачу успішно видалено.");
                _navigationService.NavigateTo(typeof(TasksPage));
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
    }
}
