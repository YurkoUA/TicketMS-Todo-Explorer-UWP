using System;

namespace TMS.TodoApi.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo(Type page);
        void NavigateTo(Type page, object parameter);
        void GoBack();
    }
}
