using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BindTextBox
{
    public class Class1 : BindableBase
    {
        //private Model _model;    

        public DelegateCommand SubtractQuantityCommand { get; }
        public DelegateCommand AddQuantityCommand { get; }
        public Class1(string currentquantity)
        {
            //_model = model;

            _quantityT = currentquantity;

            this.SubtractQuantityCommand = new DelegateCommand(SubtractQuantity, CanSubtract);
            this.AddQuantityCommand = new DelegateCommand(AddQuantity, CanAdd);
        }

        private void SubtractQuantity(object obj)
        {
            QuantityT = (int.Parse(QuantityT) - 1).ToString();
        }

        private bool CanAdd(object obj)
        {
            return int.Parse(QuantityT) < 99999;
        }
        private void AddQuantity(object obj)
        {
            QuantityT = (int.Parse(QuantityT) + 1).ToString();
        }

        private bool CanSubtract(object obj)
        {
            return int.Parse(QuantityT) > 0;
        }


        private void InvalidateCommands()
        {
            (AddQuantityCommand as DelegateCommand)?.RaiseCanExecuteChanged();
            (SubtractQuantityCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        }

        private string _quantityT;
        public string QuantityT
        {
            get { return _quantityT; }
            set
            {
                this.Set(ref _quantityT, value);
                InvalidateCommands();
            }
        }
    }

    public class DelegateCommand : IDelegateCommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        #region Constructors
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = this.AlwaysCanExecute;
        }
        #endregion

        #region IDelegateCommand
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion

        bool AlwaysCanExecute(object param)
        {
            return true;
        }
    }
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
