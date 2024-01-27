using System.Windows.Input;

namespace EzShell.Maui;

public static class EzShellPageExtensions
{
    // Attached Properties for ViewModel Type
    
    public static readonly BindableProperty ViewModelTypeProperty =
        BindableProperty.CreateAttached(
            "ViewModelType",
            typeof(Type),
            typeof(EzShellPageExtensions),
            default(Type),
            propertyChanged: OnViewModelTypePropertyChanged);
    
    private static void OnViewModelTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ContentPage page)
            return;

        if (newValue is not Type viewModelType)
            return;
        
        try
        {
            if (EzShellMauiBuilderExtensions.ServiceProvider == null)
            {
                var viewModelInstance = Activator.CreateInstance(viewModelType);
                page.BindingContext = viewModelInstance;
            }
            else
            {
                var viewModelInstance = ActivatorUtilities.CreateInstance(EzShellMauiBuilderExtensions.ServiceProvider, viewModelType);
                page.BindingContext = viewModelInstance;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static Type GetViewModelType(BindableObject obj)
    {
        return (Type)obj.GetValue(ViewModelTypeProperty);
    }
    
    public static void SetViewModelType(BindableObject obj, Type value)
    {
        obj.SetValue(ViewModelTypeProperty, value);
    }
    
    // Attached Properties for OnAppearing Events
    
    public static readonly BindableProperty OnAppearingCommandProperty =
        BindableProperty.CreateAttached(
            "OnAppearingCommand",
            typeof(ICommand),
            typeof(EzShellPageExtensions),
            null,
            propertyChanged: OnOnAppearingCommandChanged);

    public static ICommand GetOnAppearingCommand(BindableObject obj)
    {
        return (ICommand)obj.GetValue(OnAppearingCommandProperty);
    }

    public static void SetOnAppearingCommand(BindableObject obj, ICommand value)
    {
        obj.SetValue(OnAppearingCommandProperty, value);
    }

    private static void OnOnAppearingCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ContentPage page) 
            return;
        
        if (oldValue is ICommand)
            page.Appearing -= OnPageAppearing;

        if (newValue is ICommand)
            page.Appearing += OnPageAppearing;
    }

    private static void OnPageAppearing(object sender, EventArgs e)
    {
        if (sender is not BindableObject bindable) 
            return;
        
        var command = GetOnAppearingCommand(bindable);
        command.Execute(null);
    }
    
    // Attached properties for OnDisappering events
    
    public static readonly BindableProperty OnDisappearingCommandProperty =
        BindableProperty.CreateAttached(
            "OnDisappearingCommand",
            typeof(ICommand),
            typeof(EzShellPageExtensions),
            null,
            propertyChanged: OnOnDisappearingCommandChanged);

    public static ICommand GetOnDisappearingCommand(BindableObject obj)
    {
        return (ICommand)obj.GetValue(OnDisappearingCommandProperty);
    }

    public static void SetOnDisappearingCommand(BindableObject obj, ICommand value)
    {
        obj.SetValue(OnDisappearingCommandProperty, value);
    }

    private static void OnOnDisappearingCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ContentPage page) 
            return;
        
        if (oldValue is ICommand)
            page.Disappearing -= OnPageDisappearing;

        if (newValue is ICommand)
            page.Disappearing += OnPageDisappearing;
    }

    private static void OnPageDisappearing(object sender, EventArgs e)
    {
        if (sender is not BindableObject bindable) 
            return;
        
        var command = GetOnDisappearingCommand(bindable);
        command.Execute(null);
    }
}