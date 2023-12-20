namespace EZShell;

public class EzShellNavigation
{
    private static Shell CustomShell { get; set; }

    public static EzShellNavigation Instance { get; } = new();

    public object NavigationParameter { get; set; }
    public bool IsReverseNavigation { get; set; }

    static EzShellNavigation()
    { }

    private EzShellNavigation()
    { }

    public static void Initialize(Shell shell)
    {
        CustomShell = shell ?? throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        CustomShell.Navigated += CustomShellNavigated;
    }

    private static void CustomShellNavigated(object sender, ShellNavigatedEventArgs e)
    {
        Instance?.OnShellNavigated();
    }

    private void OnShellNavigated()
    {
        if ((CustomShell?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage is ContentPage 
            { 
                BindingContext: IEzShellViewModel viewModel
            })
        {
            if (IsReverseNavigation)
                viewModel.ReverseInitAsync(NavigationParameter);
            else
                viewModel.InitializeAsync(NavigationParameter);
        }

        NavigationParameter = null;
        IsReverseNavigation = false;
    }

    public async Task ChangeTabAsync(int tabIndex, object tParameter, bool popToRootFirst)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        if (popToRootFirst)
            await CustomShell.Navigation.PopToRootAsync(false);
        
        NavigationParameter = tParameter;

        if (CustomShell?.Items[0]?.Items?.Count < tabIndex)
        {
            return;
        }

        var shellSections = CustomShell?.Items[0]?.Items;
        if (shellSections != null)
        {
            var itemTo = shellSections[tabIndex];
            CustomShell.CurrentItem = itemTo;
        }
    }

    public Task GoToAsync<TParameter>(ShellNavigationState state, TParameter tParameter, bool animate = true)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);

        NavigationParameter = tParameter;
        return CustomShell?.GoToAsync(state, animate);
    }

    public Task PopAsync<TResult>(TResult tResult, bool animate = true)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        IsReverseNavigation = true;
        NavigationParameter = tResult;
        return CustomShell?.GoToAsync("..", animate);
    }

    public Task PopToRootAsync<TResult>(TResult tResult, bool animate = true)
    {
        if (CustomShell == null)
            throw new NullReferenceException(EzShellConstants.NullShellRefExceptionText);
        
        IsReverseNavigation = true;
        NavigationParameter = tResult;
        return CustomShell?.Navigation?.PopToRootAsync(animate);
    }

    public async Task PushMultiStackAsync<TParameter>(List<ShellNavigationState> states, TParameter tParameter, bool animate, bool animateAllPages)
    {
        if (states == null || states.Count == 0)
            throw new NullReferenceException(EzShellConstants.NavigationStatesExceptionText);
        
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
        if (page is null) 
            throw new NullReferenceException(EzShellConstants.NullContentPageExceptionText);
        
        var nav = new NavigationPage(page);
        return CustomShell.Navigation.PushModalAsync(nav);

    }

    public async Task PushModalWithNavigation<TParameter>(ContentPage page, TParameter tParameter, bool animate = true)
    {
        if (page != null)
        {
            var nav = new NavigationPage(page);
            await CustomShell.Navigation.PushModalAsync(nav, animate);
            if (page.BindingContext is IEzShellViewModel vm)
            {
                vm.SetParameter(tParameter);
                await vm.InitializeAsync();
            }
        }
        else
        {
            throw new NullReferenceException(EzShellConstants.NullContentPageExceptionText);
        }
    }
}