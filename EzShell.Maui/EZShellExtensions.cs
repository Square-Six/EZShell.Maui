namespace EzShell.Maui;

/// <summary>
/// Provides extension methods for the Shell class to simplify navigation operations.
/// </summary>
public static class EzShellExtensions
{
    /// <summary>
    /// Asynchronously navigates to the specified ShellNavigationState.
    /// </summary>
    /// <param name="shell">The Shell instance.</param>
    /// <param name="state">The ShellNavigationState to navigate to.</param>
    /// <param name="parameter">An optional parameter to pass to the target page.</param>
    /// <param name="animate">A flag indicating whether to animate the navigation. Default is true.</param>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    public static Task PushAsync(this Shell shell, ShellNavigationState state, object? parameter = null,
        bool animate = true)
    {
        return EzShellNavigation.Instance.PushAsync(shell, state, parameter, animate);
    }

    /// <summary>
    /// Pops the current page from the navigation stack, returning to the previous page.
    /// </summary>
    /// <param name="shell">The Shell instance.</param>
    /// <param name="parameter">The optional parameter to pass to the previous page.</param>
    /// <param name="animate">Whether to animate the navigation transition. The default is true.</param>
    /// <returns>A task that represents the asynchronous operation of popping the page. The task completes when the navigation is finished.</returns>
    public static Task PopAsync(this Shell shell, object? parameter = null, bool animate = true)
    {
        return EzShellNavigation.Instance.PopAsync(shell, parameter, animate);
    }
    
    public static Task PopToRootAsync(this Shell shell, object? parameter = null, bool animate = true)
    {
        return EzShellNavigation.Instance.PopToRootAsync(shell, parameter, animate);
    }

    /// <summary>
    /// Changes the active tab of the Shell and navigates to the specified tab index asynchronously.
    /// </summary>
    /// <param name="tabIndex">The index of the tab to navigate to.</param>
    /// <param name="parameter">An optional parameter to pass to the tab navigation.</param>
    /// <param name="popToRootFirst">A flag indicating whether to pop to the root of the navigation stack before changing the tab.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static Task ChangeTabAsync(this Shell shell, int tabIndex, object? parameter = null,
        bool popToRootFirst = true)
    {
        return EzShellNavigation.Instance.ChangeTabAsync(shell, tabIndex, parameter, popToRootFirst);
    }

    /// <summary>
    /// Pushes multiple views onto the navigation stack asynchronously.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter to be passed to the views.</typeparam>
    /// <param name="shell">The Shell instance.</param>
    /// <param name="states">A list of ShellNavigationState representing the views to be pushed.</param>
    /// <param name="parameter">An optional parameter to be passed to the views.</param>
    /// <param name="animate">A bool value indicating whether to animate the transition.</param>
    /// <param name="animateAllPages">A bool value indicating whether to animate all pages in the navigation stack.</param>
    /// <returns>A Task that represents the asynchronous push operation.</returns>
    public static Task PushMultiStackAsync(this Shell shell, List<ShellNavigationState> states,
        object? parameter = null, bool animate = true, bool animateAllPages = false)
    {
        return EzShellNavigation.Instance.PushMultiStackAsync(shell, states, parameter, animate, animateAllPages);
    }

    /// <summary>
    /// Pushes a modal page onto the navigation stack.
    /// </summary>
    /// <param name="shell">The Shell instance.</param>
    /// <param name="page">The modal page to push.</param>
    /// <param name="parameter">An optional parameter to pass to the page.</param>
    /// <param name="animate">A flag indicating whether to animate the transition. The default value is true.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static Task PushModalAsync(this Shell shell, ContentPage page, object? parameter = null, bool animate = true)
    {
        return EzShellNavigation.Instance.PushModalWithNavigation(shell, page, parameter, animate);
    }
}