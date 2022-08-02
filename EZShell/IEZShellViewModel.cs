using System;
using System.Threading.Tasks;

namespace EZShell
{
    public interface IEZShellViewModel
    {
        object Parameter { get; set; }
        object ReversParameter { get; set; }
        Task InitializeAsync();
        Task InitializeAsync(object parameter);
        Task ReverseInitAsync(object parameter);
        void SetParameter(object parameter);
        void SetReverseParameter(object parameter);
    }
}

