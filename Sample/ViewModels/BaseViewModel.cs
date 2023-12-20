using System;
using System.Threading.Tasks;
using ShellNavigation.Models;
using EZShell;

namespace EZShellSample
{
    public class BaseViewModel : BasePropertyChangedModel, IEzShellViewModel
    {
        public object Parameter { get; set; }
        public object ReversParameter { get; set; }

        public BaseViewModel()
        {
        }

        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        public virtual Task ReverseInitAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        public virtual void SetParameter(object parameter)
        {
            Parameter = parameter;
        }

        public virtual void SetReverseParameter(object parameter)
        {
            ReversParameter = parameter;
        }
    }
}

