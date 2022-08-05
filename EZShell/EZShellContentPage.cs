using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Maui.Controls;

namespace EZShell;

public class EZShellContentPage : ContentPage
{
    public Type ViewModelType
    {
        get => (Type)GetValue(ViewModelTypeProperty);
        set => SetValue(ViewModelTypeProperty, value);
    }

    public static readonly BindableProperty ViewModelTypeProperty = BindableProperty.Create(
                                                    propertyName: nameof(ViewModelType),
                                                    returnType: typeof(Type),
                                                    declaringType: typeof(EZShellContentPage),
                                                    defaultBindingMode: BindingMode.TwoWay,
                                                    propertyChanged: OnViewModelTypePropertyChanged);

    public EZShellContentPage()
	{
        
    }

    private static void OnViewModelTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is EZShellContentPage control)
        {
            if (newValue is Type type)
            {
                control.ViewModelType = type;

                // Set the pages binding context
                var context = IOCHelper.GetService(type);
                if (context != null)
                {
                    control.BindingContext = context;
                }
                else
                {
                    var assemblyName = type?.Assembly?.GetName()?.Name;
                    var typeName = $"{assemblyName}.{type.Name}";
                    var instance = Activator.CreateInstance(assemblyName, typeName)?.Unwrap();
                    if (instance != null)
                    {
                        control.BindingContext = instance;
                    }
                    else
                    {
                        throw new Exception($"Unable to create binding context for: {type.Name}.");
                    }
                }
            }
        }
    }
}
