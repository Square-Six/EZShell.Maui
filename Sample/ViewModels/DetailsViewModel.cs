using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShellNavigation.Tools;
using EZShell;

namespace EZShellSample
{
    public class DetailsViewModel : BaseViewModel
    {
        public string Value { get; set; }

        private AsyncCommand _pushCommand;
        public AsyncCommand PushCommand => _pushCommand ??= new AsyncCommand(OnPushNavigation);

        public DetailsViewModel()
        {
        }

        public override Task InitializeAsync(object parameter)
        {
            if (parameter is List<string> list)
            {
                Value = string.Join(", ", list);
            }

            return base.InitializeAsync(parameter);
        }

        private Task OnPushNavigation()
        {
            return Shell.Current.GoToAsync(nameof(ThirdDeepPage));
        }
    }
}
