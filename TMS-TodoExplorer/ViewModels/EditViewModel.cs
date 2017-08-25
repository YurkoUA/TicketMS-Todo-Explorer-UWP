using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TMS.TodoApi.Exceptions;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;
using TMS.TodoExplorer.Commands;

namespace TMS.TodoExplorer.ViewModels
{
    public class EditViewModel : BaseViewModel
    {
        public EditViewModel(ITodoService todoService, INavigationService navigationServ)
        {
            _todoService = todoService;
            _navigationService = navigationServ;
        }

        private ITodoService _todoService;
        private INavigationService _navigationService;

        private ICommand _saveChangesCommand;

        public TodoTask SelectedTask { get; set; }

        public string Title
        {
            get => SelectedTask.Title;
            set
            {
                SelectedTask.Title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get => SelectedTask.Description;
            set
            {
                SelectedTask.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public ICommand SaveChangesCommand
        {
            get => _saveChangesCommand ?? (_saveChangesCommand = new SimpleCommand(async obj =>
            {
                await SaveChanges();
            }));
        }

        public async Task SaveChanges()
        {
            try
            {
                await _todoService.UpdateAsync(SelectedTask.Id, SelectedTask.Title, SelectedTask.Description);

                await MessageBox.ShowAsync("Задачу успішно відредаговано.");
                _navigationService.GoBack();
            }
            catch (NotFoundException)
            {
                await MessageBox.ShowAsync("Задачу не знайдено.\nМожливо, її хтось видалив.", "Помилка");
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
