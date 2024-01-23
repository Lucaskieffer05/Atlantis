using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using ExcelDataReader;

namespace Atlantis.ViewModels
{
    class ViewModelCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public ViewModelCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public ViewModelCommand(Action<object> execute): this(execute, null){}

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }


        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        
    }
}
