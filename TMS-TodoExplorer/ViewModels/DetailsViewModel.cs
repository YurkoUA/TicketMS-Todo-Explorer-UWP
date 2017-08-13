using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;
using Windows.UI.Xaml;

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
    }
}
