using System;
using System.Windows.Input;

namespace GUI.Commands
{
    internal class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action<object?> _execute;

        private readonly Predicate<object?>? _canExcute;

        #endregion



        #region Properties

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion



        #region Constructors

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExcute)
        {
            _execute = execute;
            _canExcute = canExcute;
        }

        public RelayCommand(Action<object?> excute)
        {
            _execute = excute;
        }

        #endregion



        #region Methods

        public bool CanExecute(object? parameter)
        {
            return _canExcute==null? true : _canExcute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
