using System;
using System.Threading.Tasks;
using ShellNavigation.Models;
using ShellNavigation.Tools;
using EZShell;

namespace EZShellSample
{
    public class DetailsModalViewModel : BaseViewModel
    {
        public string Value { get; set; }

        private AsyncCommand _closeCommand;
        public AsyncCommand CloseCommand => _closeCommand ??= new AsyncCommand(CloseAsync);

        public DetailsModalViewModel()
        {
        }

        public override Task InitializeAsync(object parameter)
        {
            if (parameter is string value)
            {
                Value = value;
            }

            return base.InitializeAsync(parameter);
        }

        private Task CloseAsync()
        {
            var data = new Item
            {
                Description = "test",
                Id = "1",
                Text = "Testing Text"
            };

            return Shell.Current.PopAsync(data);
        }
    }
}
