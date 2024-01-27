namespace EzShell.Maui;

public class EzShellNavigation
{
    private static Shell? CustomShell { get; set; }

    public static EzShellNavigation Instance { get; } = new();

    private object? _navigationParameter;

    private bool _isReverseNavigation;
    
    public static void Initialize(Shell shell)
    {
        CustomShell = shell ?? throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        CustomShell.Navigated += CustomShellNavigated!;
    }

    private static void CustomShellNavigated(object sender, ShellNavigatedEventArgs e)
    {
        Instance.OnShellNavigated();
    }

    private void OnShellNavigated()
    {
        if ((CustomShell?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage is ContentPage 
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

    public async Task ChangeTabAsync(int tabIndex, object? tParameter, bool popToRootFirst)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        if (popToRootFirst)
            await CustomShell.Navigation.PopToRootAsync(false);
        
        _navigationParameter = tParameter;

        if (CustomShell.Items[0]?.Items?.Count < tabIndex)
        {
            return;
        }

        var shellSections = CustomShell?.Items[0]?.Items;
        if (shellSections != null)
        {
            var itemTo = shellSections[tabIndex];
            CustomShell!.CurrentItem = itemTo;
        }
    }

    public Task GoToAsync<TParameter>(ShellNavigationState state, TParameter tParameter, bool animate = true)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);

        _navigationParameter = tParameter;
        return CustomShell.GoToAsync(state, animate);
    }

    public Task PopAsync<TResult>(TResult tResult, bool animate = true)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        _isReverseNavigation = true;
        _navigationParameter = tResult;
        return CustomShell.GoToAsync("..", animate);
    }

    public Task PopToRootAsync<TResult>(TResult tResult, bool animate = true)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        _isReverseNavigation = true;
        _navigationParameter = tResult;
        return CustomShell.Navigation.PopToRootAsync(animate);
    }

    public async Task PushMultiStackAsync<TParameter>(List<ShellNavigationState> states, TParameter tParameter, bool animate, bool animateAllPages)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        if (states == null || states.Count == 0)
            throw new NullReferenceException(EzShellConstants.NavigationStatesExceptionText);
        
        var lastState = states.Last();
        foreach (var state in states)
        {
            if (state == lastState)
            {
                _navigationParameter = tParameter;
                await CustomShell.GoToAsync(state, animate);
            }
            else
            {
                await CustomShell.GoToAsync(state, animateAllPages);
            }
        }
    }

    public async Task PushModalWithNavigation<TParameter>(ContentPage page, TParameter? tParameter, bool animate = true)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        if (page == null)
            throw new NullReferenceException(EzShellConstants.NullContentPageExceptionText); 
        
        await CustomShell.Navigation.PushModalAsync(new NavigationPage(page), animate);
        if (page.BindingContext is IEzShellViewModel vm)
        {
            if (tParameter != null)
                _= vm.DataReceivedAsync(tParameter);
        }
    }
}