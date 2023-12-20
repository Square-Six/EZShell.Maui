using System;
using System.Threading.Tasks;
using ShellNavigation.Models;
using ShellNavigation.Tools;
using EZShell;

namespace EZShellSample
{
    public class PopNavTestViewModel : BaseViewModel
    {
        private AsyncCommand _goBackCommand;
        public AsyncCommand GoBackCommand => _goBackCommand ??= new AsyncCommand(GoBackAsync);

        public PopNavTestViewModel()
        {
        }

        private Task GoBackAsync()
        {
            var data = new Item
            {
                Description = "Normal Pop Test",
                Id = "4",
                Text = "Normal Pop Test Text"
            };

            return Shell.Current.PopAsync(data);
        }
    }
}
