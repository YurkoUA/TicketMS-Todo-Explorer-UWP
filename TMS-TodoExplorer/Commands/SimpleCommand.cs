using System;
using System.Windows.Input;

namespace TMS.TodoExplorer.Commands
{
    public class SimpleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> execute;
        private Func<object, bool> canExecute;

        public SimpleCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
