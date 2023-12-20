namespace EZShell;

public static class EzShellExtensions
{
    public static Task PushMultiStackAsync(this Shell shell, List<ShellNavigationState> states, object parameter, bool animate = true, bool animateAllPages = false)
    {
        if (shell == null || EzShellNavigation.Instance == null)
            return Task.CompletedTask;
        
        return EzShellNavigation.Instance.PushMultiStackAsync(states, parameter, animate, animateAllPages);
    }

    public static Task GoToAsync(this Shell shell, ShellNavigationState state, object parameter, bool animate = true)
    {
        if (shell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        if (EzShellNavigation.Instance != null)
            EzShellNavigation.Instance.GoToAsync(state, parameter, animate);

        return Task.CompletedTask;
    }

    public static Task PopAsync(this Shell shell, bool animate = true)
    {
        return shell.GoToAsync("..", animate);
    }

    public static Task PopAsync(this Shell shell, object parameter, bool animate = true)
    {
        if (shell == null || EzShellNavigation.Instance == null)
            return Task.CompletedTask;

        return EzShellNavigation.Instance.PopAsync(parameter, animate);
    }

    public static Task PopToRootAsync(this Shell shell, object parameter, bool animate = true)
    {
        if (shell == null || EzShellNavigation.Instance == null)
            return Task.CompletedTask;
        
        return EzShellNavigation.Instance.PopToRootAsync(parameter, animate);
    }

    public static Task ChangeTabAsync(this Shell shell, int tabIndex, object parameter, bool popToRootFirst = true)
    {
        if (shell == null || EzShellNavigation.Instance == null)
            return Task.CompletedTask;

        return EzShellNavigation.Instance.ChangeTabAsync(tabIndex, parameter, popToRootFirst);
    }

    public static Task PushModalWithNavigation(this Shell shell, ContentPage page)
    {
        if (shell == null || EzShellNavigation.Instance == null)
            return Task.CompletedTask;
        
        return EzShellNavigation.Instance.PushModalWithNavigation(page);
    }

    public static Task PushModalWithNavigation(this Shell shell, ContentPage page, object parameter, bool animate = true)
    {
        if (shell == null || EzShellNavigation.Instance == null)
            return Task.CompletedTask;
        
        return EzShellNavigation.Instance.PushModalWithNavigation(page, parameter, animate);
    }
}