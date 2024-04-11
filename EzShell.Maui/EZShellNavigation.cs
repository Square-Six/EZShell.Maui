namespace EzShell.Maui;

/// <summary>
/// Provides navigation methods for Shell-based navigation in a Maui application.
/// </summary>
public class EzShellNavigation
{
    /// <summary>
    /// Provides a singleton instance of the <see cref="EzShellNavigation"/> class for Shell-based navigation in a Maui application.
    /// </summary>
    public static EzShellNavigation Instance { get; } = new();

    /// <summary>
    /// Represents the parameter passed during navigation in a Shell-based Maui application.
    /// </summary>
    private object? _navigationParameter;

    /// <summary>
    /// Indicates whether the navigation is being performed in reverse.
    /// </summary>
    private bool _isReverseNavigation;

    /// <summary>
    /// Represents a Shell instance used for navigation in a Maui application.
    /// </summary>
    /// <remarks>
    /// The <see cref="_shell"/> variable holds a reference to the Shell instance, which is used for navigating between pages
    /// in a Shell-based navigation architecture. It is set during the Shell setup process, and should be null when the Shell is not available.
    /// </remarks>
    private Shell? _shell;

    /// <summary>
    /// Sets up the shell for navigation.
    /// </summary>
    /// <param name="shell">The Shell instance to be set up.</param>
    /// <exception cref="NullReferenceException">Thrown if the given shell is null.</exception>
    private void ShellSetup(Shell shell)
    {
        if (shell == null)
            throw new NullReferenceException(EzShellConstants.ShellNotFoundText);

        shell.Navigated += OnShellNavigated!;
        _shell = shell;
    }

    /// <summary>
    /// Teardown method for Shell navigation.
    /// </summary>
    /// <param name="shell">The Shell instance to teardown.</param>
    private void ShellTeardown(Shell shell)
    {
        shell.Navigated -= OnShellNavigated!;
        _shell = null;
    }

    /// <summary>
    /// Registers a route for a page type.
    /// </summary>
    /// <param name="pageType">The type of the page to register the route for.</param>
    private void RegisterRoute(Type pageType)
    {
        Routing.UnRegisterRoute(pageType.Name);
        Routing.RegisterRoute(pageType.Name, pageType);
    }

    /// <summary>
    /// Event handler for the Shell.Navigated event.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    private void OnShellNavigated(object sender, ShellNavigatedEventArgs e)
    {
        if ((_shell?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage is ContentPage 
            { 
                BindingContext: IEzShellViewModel viewModel
            })
        {
            if (_isReverseNavigation)
                viewModel.ReverseDataReceivedAsync(_navigationParameter!);
            else
                viewModel.DataReceivedAsync(_navigationParameter!);
        }

        _navigationParameter = null;
        _isReverseNavigation = false;
    }

    /// <summary>
    /// Pushes a new page to the Shell navigation stack asynchronously.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter to be passed during navigation.</typeparam>
    /// <param name="shell">The Shell instance to push the page to.</param>
    /// <param name="state">The ShellNavigationState representing the page to be pushed.</param>
    /// <param name="tParameter">The parameter to be passed during navigation.</param>
    /// <param name="animate">True to animate the page transition, false otherwise.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task PushAsync<TParameter>(Shell shell, ShellNavigationState state, TParameter? tParameter, bool animate = true)
    {
        ShellSetup(shell);
        _navigationParameter = tParameter;
        await Shell.Current.GoToAsync(state, animate);
        ShellTeardown(shell);
    }

    /// <summary>
    /// Pushes a page onto the navigation stack asynchronously.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter for the page.</typeparam>
    /// <param name="shell">The Shell instance to navigate on.</param>
    /// <param name="pageType">The type of the page to push.</param>
    /// <param name="tParameter">The parameter for the page.</param>
    /// <param name="animate">True to animate the transition, false otherwise. Default is true.</param>
    /// <returns>A Task representing the ongoing asynchronous operation.</returns>
    public async Task PushAsync<TParameter>(Shell shell, Type pageType, TParameter? tParameter, bool animate = true)
    {
        RegisterRoute(pageType);
        ShellSetup(shell);
        _navigationParameter = tParameter;
        await Shell.Current.GoToAsync(new ShellNavigationState(pageType.Name), animate);
        ShellTeardown(shell);
    }
    
    /// <summary>
    /// Changes the currently selected tab in a Shell-based Maui application.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter passed during navigation.</typeparam>
    /// <param name="shell">The Shell instance.</param>
    /// <param name="tabIndex">The index of the tab to be selected.</param>
    /// <param name="tParameter">The parameter passed during navigation.</param>
    /// <param name="popToRootFirst">A flag indicating whether to pop to the root before changing the tab.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ChangeTabAsync<TParameter>(Shell shell, int tabIndex, TParameter? tParameter, bool popToRootFirst)
    {
        ShellSetup(shell);

        if (popToRootFirst)
            await Shell.Current.Navigation.PopToRootAsync(false);
        
        _navigationParameter = tParameter;

        if (Shell.Current.Items[0]?.Items?.Count < tabIndex)
            return;

        var shellSections = Shell.Current.Items[0]?.Items;
        if (shellSections != null)
        {
            var itemTo = shellSections[tabIndex];
            Shell.Current.CurrentItem = itemTo;
        }
        
        ShellTeardown(shell);
    }

    /// <summary>
    /// Asynchronously pops the topmost page from the navigation stack and returns a result value.
    /// </summary>
    /// <typeparam name="TResult">The type of the result value.</typeparam>
    /// <param name="shell">The Shell instance to perform the pop operation on.</param>
    /// <param name="tResult">The result value.</param>
    /// <param name="animate">Whether to animate the pop transition. Default is true.</param>
    /// <returns>A task representing the asynchronous pop operation.</returns>
    public async Task PopAsync<TResult>(Shell shell, TResult tResult, bool animate = true)
    {
        ShellSetup(shell);

        _isReverseNavigation = true;
        _navigationParameter = tResult;
        await Shell.Current.GoToAsync("..", animate);
        
        ShellTeardown(shell);
    }

    /// <summary>
    /// Pops all pages from the navigation stack and returns to the root page.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="shell">The Shell instance to perform the navigation.</param>
    /// <param name="tResult">The result parameter to be passed during navigation.</param>
    /// <param name="animate">True to animate the navigation, false otherwise. Default is true.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task PopToRootAsync<TResult>(Shell shell, TResult tResult, bool animate = true)
    {
        ShellSetup(shell);

        _isReverseNavigation = true;
        _navigationParameter = tResult;
        await Shell.Current.Navigation.PopToRootAsync(animate);
        
        ShellTeardown(shell);
    }

    /// <summary>
    /// Pushes multiple navigation states onto the Shell navigation stack asynchronously.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter to pass to the last navigation state.</typeparam>
    /// <param name="shell">The Shell instance.</param>
    /// <param name="states">A list of ShellNavigationState objects representing the navigation states to push.</param>
    /// <param name="tParameter">The parameter to pass to the last navigation state.</param>
    /// <param name="animate">Specifies whether to animate the navigation transitions.</param>
    /// <param name="animateAllPages">Specifies whether to animate all the pages in the navigation stack.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="NullReferenceException">Thrown if the list of navigation states is null or empty.</exception>
    public async Task PushMultiStackAsync<TParameter>(Shell shell, List<ShellNavigationState> states, TParameter tParameter, bool animate, bool animateAllPages)
    {
        ShellSetup(shell);

        if (states == null || states.Count == 0)
            throw new NullReferenceException(EzShellConstants.NavigationStatesExceptionText);

        var lastState = states.Last();
        foreach (var state in states)
        {
            if (state == lastState)
            {
                _navigationParameter = tParameter;
                await Shell.Current.GoToAsync(state, animate);
            }
            else
            {
                await Shell.Current.GoToAsync(state, animateAllPages);
            }
        }
        
        ShellTeardown(shell);
    }

    /// <summary>
    /// Pushes multiple pages onto the Shell navigation stack asynchronously.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter to pass to the pages.</typeparam>
    /// <param name="shell">The Shell instance.</param>
    /// <param name="pageTypes">The list of page types to navigate to.</param>
    /// <param name="tParameter">The parameter to pass to the pages.</param>
    /// <param name="animate">A boolean value indicating whether to animate the navigation.</param>
    /// <param name="animateAllPages">A boolean value indicating whether to animate all pages during the navigation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NullReferenceException">Thrown when the pageTypes parameter is null or empty.</exception>
    public async Task PushMultiStackAsync<TParameter>(Shell shell, List<Type> pageTypes, TParameter tParameter, bool animate, bool animateAllPages)
    {
        ShellSetup(shell);

        if (pageTypes == null || pageTypes.Count == 0)
            throw new NullReferenceException(EzShellConstants.NavigationStatesExceptionText);

        var lastState = pageTypes.Last();
        foreach (var type in pageTypes)
        {
            RegisterRoute(type);
            if (type == lastState)
            {
                _navigationParameter = tParameter;
                await Shell.Current.GoToAsync(new ShellNavigationState(type.Name), animate);
            }
            else
            {
                await Shell.Current.GoToAsync(new ShellNavigationState(type.Name), animateAllPages);
            }
        }
        
        ShellTeardown(shell);
    }

    /// <summary>
    /// Pushes a ContentPage as a modal with navigation in a Shell-based navigation in a Maui application.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter to be passed to the view model associated with the page. Pass null if no parameter needs to be passed.</typeparam>
    /// <param name="shell">The Shell instance.</param>
    /// <param name="page">The ContentPage to be pushed.</param>
    /// <param name="tParameter">The parameter to be passed to the view model associated with the page.</param>
    /// <param name="animate">Specifies whether the navigation transition should be animated or not. Default value is true.</param>
    /// <exception cref="NullReferenceException">Thrown if the given shell is null or the given page is null.</exception>
    public async Task PushModalWithNavigation<TParameter>(Shell shell, ContentPage page, TParameter? tParameter, bool animate = true)
    {
        ShellSetup(shell);

        if (page == null)
        {
            throw new NullReferenceException(EzShellConstants.NullContentPageExceptionText);
        }

        await Shell.Current.Navigation.PushModalAsync(new NavigationPage(page), animate);
        if (page.BindingContext is IEzShellViewModel vm)
        {
            if (tParameter != null)
                _= vm.DataReceivedAsync(tParameter);
        }
        
        ShellTeardown(shell);
    }
}