using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShellNavigation.Models;
using ShellNavigation.Tools;
using EZShell;

namespace EZShellSample
{
    public class SettingsViewModel : BaseViewModel
    {
        public string TabChangeParameter { get; set; }

        private AsyncCommand _multiNavCommandAsync;
        public AsyncCommand MultiNavCommandAsync => _multiNavCommandAsync ??= new AsyncCommand(OnMultiNavCommandAsync);

        private AsyncCommand _modalNavCommand;
        public AsyncCommand ModalNavCommand => _modalNavCommand ??= new AsyncCommand(OnModalNavCommandAsync);

        public SettingsViewModel()
        {
        }

        public override Task InitializeAsync(object parameter)
        {
            if (parameter is string value)
            {
                TabChangeParameter = value;
            }

            return base.InitializeAsync(parameter);
        }

        private Task OnMultiNavCommandAsync()
        {
            var pages = new List<ShellNavigationState>
            {
                nameof(DetailsPage),
                nameof(ThirdDeepPage),
                nameof(MultiNavFinalPage)
            };

            var data = new Item
            {
                Description = "multi nav test",
                Id = "5",
                Text = "multi nav test Text"
            };

            return Shell.Current.PushMultiStackAsync(pages, data);
        }

        private Task OnModalNavCommandAsync()
        {
            //return Shell.Current.PushModalWithNavStackAsync(new DetailsModalPage());
            return Shell.Current.PushModalWithNavigation(new DetailsModalPage(), "Test modal push with params");
        }
    }
}
