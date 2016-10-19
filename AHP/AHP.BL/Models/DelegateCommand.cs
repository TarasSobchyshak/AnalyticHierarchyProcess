using System;
using System.Windows.Input;

namespace AHP.BL.Models
{
    public class DelegateCommand : ICommand
    {
        Action _execute;
        Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public DelegateCommand(Action execute)
        {
            _execute = execute;
            _canExecute = AlwaysCanExecute;
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }
        private bool AlwaysCanExecute() => true;
    }
}
