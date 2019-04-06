using System;
using System.Windows.Input;

namespace BDC_V1.Utils
{
    public class RelayCommand:ICommand
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add    => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public Func  <object, bool > ValidateExecution { get; set; }
        public Action<object,object> ExecuteCommand    { get; set; }

        public RelayCommand(Func<object,bool> validateExecution, Action<object,object> executeCommand)
        {
            ValidateExecution = validateExecution;
            ExecuteCommand    = executeCommand;
        }

        public RelayCommand( Action<object,object> executeCommand)
        {
            ExecuteCommand = executeCommand;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return this.ValidateExecution == null || this.ValidateExecution(parameter);
        }

        void ICommand.Execute(object sender)
        {
            this.ExecuteCommand(sender, null);           
        }
    }
}
