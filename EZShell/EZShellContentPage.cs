namespace EZShell;

public abstract class EzShellContentPage : ContentPage
{
    public Type ViewModelType
    {
        get => (Type)GetValue(ViewModelTypeProperty);
        set => SetValue(ViewModelTypeProperty, value);
    }

    public static readonly BindableProperty ViewModelTypeProperty = BindableProperty.Create(
                                                    propertyName: nameof(ViewModelType),
                                                    returnType: typeof(Type),
                                                    declaringType: typeof(EzShellContentPage),
                                                    defaultBindingMode: BindingMode.TwoWay,
                                                    propertyChanged: OnViewModelTypePropertyChanged);

    private static void OnViewModelTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not EzShellContentPage control) 
            return;

        if (newValue is not Type type) 
            return;
        
        control.ViewModelType = type;

        // Set the pages binding context
        var context = IocHelper.GetService(type);
        if (context != null)
        {
            control.BindingContext = context;
        }
        else
        {
            var assemblyName = type.Assembly.GetName().Name;
            var typeName = $"{assemblyName}.{type.Name}";
            var instance = Activator.CreateInstance(assemblyName, typeName)?.Unwrap();
            if (instance != null)
                control.BindingContext = instance;
            else
                throw new Exception($"Unable to create binding context for: {type.Name}.");
        }
    }
}
