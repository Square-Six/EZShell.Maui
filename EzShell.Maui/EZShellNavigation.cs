namespace EzShell.Maui;

public class EzShellNavigation
{
    public static EzShellNavigation Instance { get; } = new();

    private object? _navigationParameter;

    private bool _isReverseNavigation;
    
    private static void CustomShellNavigated(object sender, ShellNavigatedEventArgs e)
    {
        Instance.OnShellNavigated();
    }
    
    private static void ShellSetup()
    {
        if (Shell.Current == null)
            throw new NullReferenceException(EzShellConstants.ShellNotFoundText);
        
        Shell.Current.Navigated += CustomShellNavigated!;
    }

    private static void ShellTeardown()
    {
        Shell.Current.Navigated -= CustomShellNavigated!;
    }

    private void OnShellNavigated()
    {
        if ((Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage is ContentPage 
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
    
    public async Task PushAsync<TParameter>(ShellNavigationState state, TParameter? tParameter, bool animate = true)
    {
        ShellSetup();
        
        _navigationParameter = tParameter;
        await Shell.Current.GoToAsync(state, animate);
        
        ShellTeardown();
    }

    public async Task ChangeTabAsync<TParameter>(int tabIndex, TParameter? tParameter, bool popToRootFirst)
    {
        ShellSetup();
        
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
        
        ShellTeardown();
    }

    public async Task PopAsync<TResult>(TResult tResult, bool animate = true)
    {
        ShellSetup();
        
        _isReverseNavigation = true;
        _navigationParameter = tResult;
        await Shell.Current.GoToAsync("..", animate);
        
        ShellTeardown();
    }

    public async Task PopToRootAsync<TResult>(TResult tResult, bool animate = true)
    {
        ShellSetup();
        
        _isReverseNavigation = true;
        _navigationParameter = tResult;
        await Shell.Current.Navigation.PopToRootAsync(animate);
        
        ShellTeardown();
    }

    public async Task PushMultiStackAsync<TParameter>(List<ShellNavigationState> states, TParameter tParameter, bool animate, bool animateAllPages)
    {
        ShellSetup();
        
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
        
        ShellTeardown();
    }

    public async Task PushModalWithNavigation<TParameter>(ContentPage page, TParameter? tParameter, bool animate = true)
    {
        ShellSetup();
        
        if (page == null)
            throw new NullReferenceException(EzShellConstants.NullContentPageExceptionText); 
        
        await Shell.Current.Navigation.PushModalAsync(new NavigationPage(page), animate);
        if (page.BindingContext is IEzShellViewModel vm)
        {
            if (tParameter != null)
                _= vm.DataReceivedAsync(tParameter);
        }
        
        ShellTeardown();
    }
}