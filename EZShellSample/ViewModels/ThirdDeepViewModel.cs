using System;
using System.Threading.Tasks;
using ShellNavigation.Tools;
using ShellNavigation.Models;
using EZShell;

namespace EZShellSample
{
    public class ThirdDeepViewModel : BaseViewModel
    {
        private AsyncCommand _popCommand;
        public AsyncCommand PopCommand => _popCommand ??= new AsyncCommand(OnPopAsync);

        public ThirdDeepViewModel()
        {

        }

        private Task OnPopAsync()
        {
            var data = new Item
            {
                Description = "Third Deep Parameter",
                Id = "3",
                Text = "Third Deep Parameter Text"
            };

            return Shell.Current.PopToRootAsync(data);
        }
    }
}
