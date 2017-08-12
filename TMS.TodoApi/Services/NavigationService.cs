using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TMS.TodoApi.Interfaces;

namespace TMS.TodoApi.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateTo(Type page)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(page);
        }

        public void NavigateTo(Type page, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(page, parameter);
        }

        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.GoBack();
        }
    }
}
