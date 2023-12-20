using System;
using System.Threading.Tasks;
using ShellNavigation.Models;
using EZShell;

namespace EZShellSample
{
    public class MultiNavFinalViewModel : BaseViewModel
    {
        public string Value { get; set; }

        public MultiNavFinalViewModel()
        {
        }

        public override Task InitializeAsync(object parameter)
        {
            if (parameter is Item item)
            {
                Value = item.Text;
            }

            return base.InitializeAsync(parameter);
        }
    }
}
