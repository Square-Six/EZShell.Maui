using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShellNavigation.Models;
using ShellNavigation.Tools;
using EZShell;
using EZShellSample.Interfaces;

namespace EZShellSample
{
    public class AboutViewModel : BaseViewModel
    {
        public string ThirdParameterText { get; set; }

        private AsyncCommand _pushCommand;
        public AsyncCommand PushCommand => _pushCommand ??= new AsyncCommand(OnPushNavigation);

        private AsyncCommand _pushModalCommand;
        public AsyncCommand PushModalCommand => _pushModalCommand ??= new AsyncCommand(OnPushModal);

        private AsyncCommand _tabChangeParamCommand;
        public AsyncCommand TabChangeParamCommand => _tabChangeParamCommand ??= new AsyncCommand(ChangeTabWithParametersAsync);

        private AsyncCommand _normalPopCommand;
        public AsyncCommand NormalPopCommand => _normalPopCommand ??= new AsyncCommand(GoToPopPageAsync);

        private readonly ISampleInterface _sample;

        public AboutViewModel(ISampleInterface sample)
        {
            _sample = sample;
        }

        public override Task ReverseInitAsync(object parameter)
        {
            if (parameter is Item tResult)
            {
                ThirdParameterText = tResult.Text;
            }

            return base.ReverseInitAsync(parameter);
        }

        private Task OnPushNavigation()
        {
            var test = new List<string>
            {
                { "test 1" },
                { "test 2" }
            };

            return Shell.Current.GoToAsync(nameof(DetailsPage), test, true);
        }

        private Task OnPushModal()
        {
            return Shell.Current.GoToAsync(nameof(DetailsModalPage), "It works!");
        }

        private Task ChangeTabWithParametersAsync()
        {
            return Shell.Current.ChangeTabAsync(1, "Tab change with params work!");
        }

        private Task GoToPopPageAsync()
        {
            return Shell.Current.GoToAsync(nameof(PopNavTestView));
        }
    }
}
