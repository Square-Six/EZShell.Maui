﻿namespace EzShell.Maui;

public static class EzShellExtensions
{
    public static Task PushAsync(this Shell shell,  ShellNavigationState state, object? parameter = null, bool animate = true)
    {
        return EzShellNavigation.Instance.PushAsync(shell, state, parameter, animate);
    }
    
    public static Task PopAsync(this Shell shell, object? parameter = null, bool animate = true)
    {
        return EzShellNavigation.Instance.PopAsync(shell, parameter, animate);
    }
    
    public static Task PopToRootAsync(this Shell shell, object? parameter = null, bool animate = true)
    {
        return EzShellNavigation.Instance.PopToRootAsync(shell, parameter, animate);
    }
    
    public static Task ChangeTabAsync(this Shell shell, int tabIndex, object? parameter = null, bool popToRootFirst = true)
    {
        return EzShellNavigation.Instance.ChangeTabAsync(shell, tabIndex, parameter, popToRootFirst);
    }
    
    public static Task PushMultiStackAsync(this Shell shell, List<ShellNavigationState> states, object? parameter = null, bool animate = true, bool animateAllPages = false)
    {
        return EzShellNavigation.Instance.PushMultiStackAsync(shell, states, parameter, animate, animateAllPages);
    }
    
    public static Task PushModalAsync(this Shell shell, ContentPage page, object? parameter = null, bool animate = true)
    {
        return EzShellNavigation.Instance.PushModalWithNavigation(shell, page, parameter, animate);
    }
}