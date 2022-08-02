using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ShellNavigation.Interfaces;

namespace ShellNavigation.Tools
{
    public class AsyncCommand : IAsyncCommand, ICommand
    {
        private readonly Func<Task> _action;
        private readonly Func<bool> _predicate;
        private readonly SynchronizationContext _context;
        private event EventHandler _canExecuteChanged;

        public AsyncCommand(Func<Task> action, Func<bool> predicate = null)
        {
            _action = action;
            _predicate = predicate;
            _context = SynchronizationContext.Current;
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                await _action();
            }
        }

        public bool CanExecute()
        {
            return _predicate == null || _predicate();
        }

        public void RaiseCanExecuteChanged()
        {
            if (_context != null)
            {
                _context.Post(state => OnCanExecuteChanged(), null);
            }
            else
            {
                OnCanExecuteChanged();
            }
        }

        private void OnCanExecuteChanged()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync();
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { _canExecuteChanged += value; }
            remove { _canExecuteChanged -= value; }
        }
    }

    public class AsyncCommand<T> : IAsyncCommand<T>, ICommand
    {
        private readonly Func<T, Task> _parameterizedAction;
        private readonly Predicate<T> _canExecute;
        private readonly SynchronizationContext _context;
        private event EventHandler _canExecuteChanged;

        public AsyncCommand(Func<T, Task> parameterizedAction, Predicate<T> canExecute = null)
        {
            _parameterizedAction = parameterizedAction;
            _canExecute = canExecute;
            _context = SynchronizationContext.Current;
        }

        public async Task ExecuteAsync(T value)
        {
            if (CanExecute(value))
            {
                await _parameterizedAction(value);
            }
        }

        public bool CanExecute(T value)
        {
            return _canExecute == null || _canExecute(value);
        }

        public void RaiseCanExecuteChanged()
        {
            if (_context != null)
            {
                _context.Post(state => OnCanExecuteChanged(), null);
            }
            else
            {
                OnCanExecuteChanged();
            }
        }

        private void OnCanExecuteChanged()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync((T)parameter);
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { _canExecuteChanged += value; }
            remove { _canExecuteChanged -= value; }
        }
    }
}
