using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace EZShell
{
    public class EZShellViewModel : IEZShellViewModel
    {
        public object Parameter { get; set; }
        public object ReversParameter { get; set; }

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
