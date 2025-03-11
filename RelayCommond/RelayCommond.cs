using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppLogin.RelayCommond
{
    class Commond : ICommand
    {
        //命令是否能够执行
        readonly Func<bool> _canExecute;
        
        readonly Action _execute;
       


        public Commond(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
           if(_canExecute==null)
            { return true; }
            return _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }


    }
}
