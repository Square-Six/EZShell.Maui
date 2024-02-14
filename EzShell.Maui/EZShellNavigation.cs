namespace EzShell.Maui;

public class EzShellNavigation
{
    public static EzShellNavigation Instance { get; } = new();

    private object? _navigationParameter;

    private bool _isReverseNavigation;

    private Shell? _shell;
    
    private void ShellSetup(Shell shell)
    {
        if (shell == null)
            throw new NullReferenceException(EzShellConstants.ShellNotFoundText);

        shell.Navigated += OnShellNavigated!;
        _shell = shell;
    }

    private void ShellTeardown(Shell shell)
    {
        shell.Navigated -= OnShellNavigated!;
        _shell = null;
    }

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
    
    public async Task PushAsync<TParameter>(Shell shell, ShellNavigationState state, TParameter? tParameter, bool animate = true)
    {
        ShellSetup(shell);
        
        _navigationParameter = tParameter;
        await Shell.Current.GoToAsync(state, animate);
        
        ShellTeardown(shell);
    }

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

    public async Task PopAsync<TResult>(Shell shell, TResult tResult, bool animate = true)
    {
        ShellSetup(shell);
        
        _isReverseNavigation = true;
        _navigationParameter = tResult;
        await Shell.Current.GoToAsync("..", animate);
        
        ShellTeardown(shell);
    }

    public async Task PopToRootAsync<TResult>(Shell shell, TResult tResult, bool animate = true)
    {
        ShellSetup(shell);
        
        _isReverseNavigation = true;
        _navigationParameter = tResult;
        await Shell.Current.Navigation.PopToRootAsync(animate);
        
        ShellTeardown(shell);
    }

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

    public async Task PushModalWithNavigation<TParameter>(Shell shell, ContentPage page, TParameter? tParameter, bool animate = true)
    {
        ShellSetup(shell);
        
        if (page == null)
            throw new NullReferenceException(EzShellConstants.NullContentPageExceptionText); 
        
        await Shell.Current.Navigation.PushModalAsync(new NavigationPage(page), animate);
        if (page.BindingContext is IEzShellViewModel vm)
        {
            if (tParameter != null)
                _= vm.DataReceivedAsync(tParameter);
        }
        
        ShellTeardown(shell);
    }
}