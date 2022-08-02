using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EZShell
{
    public static class EZShellExtensions
    {
        public static Task PushMultiStackAsync(this Shell shell, List<ShellNavigationState> states, object parameter, bool animate = true, bool animateAllPages = false)
        {
            if (shell != null && EZShellNavigation.Instance != null)
            {
                return EZShellNavigation.Instance.PushMultiStackAsync(states, parameter, animate, animateAllPages);
            }

            return Task.CompletedTask;
        }

        public static Task GoToAsync(this Shell shell, ShellNavigationState state, object parameter, bool animate = true)
        {
            if (shell == null)
            {
                throw new NullReferenceException(EZShellConstants.NullShellRefExceptionText);
            }

            if (shell != null && EZShellNavigation.Instance != null)
            {
                EZShellNavigation.Instance.GoToAsync(state, parameter, animate);
            }

            return Task.CompletedTask;
        }

        public static Task PopAsync(this Shell shell, bool animate = true)
        {
            return shell.GoToAsync("..", animate);
        }

        public static Task PopAsync(this Shell shell, object parameter, bool animate = true)
        {
            if (shell != null && EZShellNavigation.Instance != null)
            {
                return EZShellNavigation.Instance.PopAsync(parameter, animate);
            }

            return Task.CompletedTask;
        }

        public static Task PopToRootAsync(this Shell shell, object parameter, bool animate = true)
        {
            if (shell != null && EZShellNavigation.Instance != null)
            {
                return EZShellNavigation.Instance.PopToRootAsync(parameter, animate);
            }

            return Task.CompletedTask;
        }

        public static Task ChangeTabAsync(this Shell shell, int tabIndex, object parameter, bool popToRootFirst = true)
        {
            if (shell != null && EZShellNavigation.Instance != null)
            {
                return EZShellNavigation.Instance.ChangeTabAsync(tabIndex, parameter, popToRootFirst);
            }

            return Task.CompletedTask;
        }

        public static Task PushModalWithNavigation(this Shell shell, ContentPage page)
        {
            if (shell != null && EZShellNavigation.Instance != null)
            {
                return EZShellNavigation.Instance.PushModalWithNavigation(page);
            }

            return Task.CompletedTask;
        }

        public static Task PushModalWithNavigation(this Shell shell, ContentPage page, object parameter, bool animate = true)
        {
            if (shell != null && EZShellNavigation.Instance != null)
            {
                return EZShellNavigation.Instance.PushModalWithNavigation(page, parameter, animate);
            }

            return Task.CompletedTask;
        }
    }
}
