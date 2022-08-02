using System;
using System.Threading.Tasks;

namespace ShellNavigation.Interfaces
{
    public interface IAsyncCommand
    {
        Task ExecuteAsync();
        bool CanExecute();
        void RaiseCanExecuteChanged();
    }

    public interface IAsyncCommand<T>
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
        void RaiseCanExecuteChanged();
    }
}
