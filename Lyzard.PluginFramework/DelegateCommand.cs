using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.PluginFramework
{
    public class PluginDelegateCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public PluginDelegateCommand(Action<object> execute) : this(execute, null)
        {

        }

        public PluginDelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
