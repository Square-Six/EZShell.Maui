using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZShell
{
    public class EZShellNavigation
    {
        private static readonly EZShellNavigation instance = new EZShellNavigation();
        private static Shell CustomShell { get; set; }

        public static EZShellNavigation Instance => instance;

        public object NavigationParameter { get; set; }
        public bool IsReverseNavigation { get; set; }

        static EZShellNavigation()
        { }

        private EZShellNavigation()
        { }

        public static void Initialize(Shell shell)
        {
            CustomShell = shell ?? throw new NullReferenceException(EZShellConstants.NullShellRefExceptionText);
            CustomShell.Navigated += CustomShellNavigated;
        }

        private static void CustomShellNavigated(object sender, ShellNavigatedEventArgs e)
        {
            Instance?.OnShellNavigated();
        }

        private void OnShellNavigated()
        {
            if ((CustomShell?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage is ContentPage basePage)
            {
                if (basePage?.BindingContext is IEZShellViewModel viewModel)
                {
                    if (IsReverseNavigation)
                    {
                        viewModel.ReverseInitAsync(NavigationParameter);
                    }
                    else
                    {
                        viewModel.InitializeAsync(NavigationParameter);
                    }
                }
            }

            NavigationParameter = null;
            IsReverseNavigation = false;
        }

        public async Task ChangeTabAsync(int tabIndex, object tParameter, bool popToRootFirst)
        {
            if (CustomShell == null)
            {
                throw new NullReferenceException(EZShellConstants.NullShellRefExceptionText);
            }

            if (popToRootFirst)
            {
                await CustomShell?.Navigation?.PopToRootAsync(false);
            }

            NavigationParameter = tParameter;

            if (CustomShell?.Items[0]?.Items?.Count < tabIndex)
            {
                return;
            }

            var itemTo = CustomShell?.Items[0]?.Items[tabIndex];
            CustomShell.CurrentItem = itemTo;
        }

        public Task GoToAsync<TParameter>(ShellNavigationState state, TParameter tParameter, bool animate = true)
        {
            if (CustomShell == null)
            {
                throw new NullReferenceException(EZShellConstants.NullShellRefExceptionText);
            }

            NavigationParameter = tParameter;
            return CustomShell?.GoToAsync(state, animate);
        }

        public Task PopAsync<TResult>(TResult tResult, bool animate = true)
        {
            if (CustomShell == null)
            {
                throw new NullReferenceException(EZShellConstants.NullShellRefExceptionText);
            }

            IsReverseNavigation = true;
            NavigationParameter = tResult;
            return CustomShell?.GoToAsync("..", animate);
        }

        public Task PopToRootAsync<TResult>(TResult tResult, bool animate = true)
        {
            if (CustomShell == null)
            {
                throw new NullReferenceException(EZShellConstants.NullShellRefExceptionText);
            }

            IsReverseNavigation = true;
            NavigationParameter = tResult;
            return CustomShell?.Navigation?.PopToRootAsync(animate);
        }

        public async Task PushMultiStackAsync<TParameter>(List<ShellNavigationState> states, TParameter tParameter, bool animate, bool animateAllPages)
        {
            if (states == null || !states.Any())
            {
                throw new NullReferenceException(EZShellConstants.NavigationStatesExceptionText);
            }

            var lastState = states.Last();
            foreach (var state in states)
            {
                if (state == lastState)
                {
                    NavigationParameter = tParameter;
                    await CustomShell.GoToAsync(state, animate);
                }
                else
                {
                    await CustomShell.GoToAsync(state, animateAllPages);
                }
            }
        }

        public Task PushModalWithNavigation(ContentPage page)
        {
            if (page is ContentPage)
            {
                var nav = new NavigationPage(page);
                return CustomShell.Navigation.PushModalAsync(nav);
            }
            else
            {
                throw new NullReferenceException(EZShellConstants.NullContentPageExceptionText);
            }
        }

        public async Task PushModalWithNavigation<TParameter>(ContentPage page, TParameter tParameter, bool animate = true)
        {
            if (page is ContentPage)
            {
                var nav = new NavigationPage(page);
                await CustomShell.Navigation.PushModalAsync(nav, animate);
                if (page.BindingContext is IEZShellViewModel vm)
                {
                    vm.SetParameter(tParameter);
                    await vm.InitializeAsync();
                }
            }
            else
            {
                throw new NullReferenceException(EZShellConstants.NullContentPageExceptionText);
            }
        }
    }
}
