using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TMS.TodoApi.Enums;
using TMS.TodoApi.Exceptions;
using TMS.TodoApi.Interfaces;
using TMS.TodoExplorer.Commands;
using TMS.TodoExplorer.Views;

namespace TMS.TodoExplorer.ViewModels
{
    public class CreateViewModel : BaseViewModel
    {
        public CreateViewModel(ITodoService todoServ, INavigationService navigServ)
        {
            _todoService = todoServ;
            _navigationService = navigServ;

            Priorities = Enum.GetValues(typeof(TaskPriority)).Cast<TaskPriority>().ToList();
            Priority = Priorities.First();
        }

        private ITodoService _todoService;
        private INavigationService _navigationService;

        private ICommand _createTaskCommand;

        private string _title;
        private string _description;
        private TaskPriority _priority;

        public List<TaskPriority> Priorities { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public TaskPriority Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public ICommand CreateTaskCommand
        {
            get
            {
                return _createTaskCommand ?? (_createTaskCommand = new SimpleCommand(async obj =>
                {
                    await CreateAsync();
                }));
            }
        }

        public async Task CreateAsync()
        {
            try
            {
                await _todoService.CreateAsync(Title, Description, Priority);

                await MessageBox.ShowAsync("Задачу успішно створено.");
                _navigationService.NavigateTo(typeof(TasksPage));
            }
            catch (BadRequestException ex)
            {
                var errors = await ex.GetErrorsAsync();
                string errorMessage;

                if (errors != null && errors.Any())
                {
                    errorMessage = string.Join("\n", errors);
                }
                else
                {
                    errorMessage = $"Помилка HTTP.\nКод: 400";
                }
                await MessageBox.ShowAsync(errorMessage, "Помилка");
            }
            catch (HttpResponseException ex)
            {
                await MessageBox.ShowAsync($"Під час запиту сталася помилка (HTTP {ex.StatusCodeNumber}).", "Помилка");
            }
            catch
            {
                await MessageBox.ShowAsync("Сталася помилка. Перевірте підключення до мережі та спробуйте ще раз.", "Помилка");
            }
        }
    }
}
