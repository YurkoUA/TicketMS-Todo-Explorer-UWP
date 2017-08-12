using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TMS.TodoExplorer.ViewModels;
using TMS.TodoExplorer.Util;
using TMS.TodoApi.Interfaces;

namespace TMS.TodoExplorer.Views
{
    public sealed partial class TasksPage : Page
    {
        public TasksPage()
        {
            InitializeComponent();
            DataContext = new TasksViewModel(AutofacConfig.Resolve<ITodoService>());
        }
    }
}
