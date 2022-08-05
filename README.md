# EZShell.Maui
EZShell.Maui allows more complex transfer of data between ViewModels using Xamarin Shell Navigation as well as Dependency Injection.

## Setup
- In your AppShell.cs class, add the following line of code.
```
EZShellNavigation.Initialize(this);
```
- Example:
```
public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		EZShellNavigation.Initialize(this);
	}
}
```

## Usage
- ContentPage 
	- Your ContentPage needs to subclass `EZShellContentPage`.
	- Set the `ViewModelType` property in the xaml. This will be your Pages' ViewModel or BindingContext. In this case its `SampleViewModel`. With setting this property, the `EZShellContentPage` will try to create and set the BindingContext based on the Type provided.
```
<ez:EZShellContentPage 
	xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:ez="clr-namespace:EZShell;assembly=EZShell"
    ViewModelType="{x:Type vm:SampleViewModel}"
    xmlns:vm="clr-namespace:EZShellSample.ViewModels"
    x:Class="EZShellSample.Views.SamplePage"
    x:DataType="vm:SampleViewModel"
    Title="SamplePage">
    <VerticalStackLayout>

    </VerticalStackLayout>
</ez:EZShellContentPage>
```
- ViewModel 
	- Your ViewModel needs to subclass `EZShellViewModel`
	- When using EZShell, there will now be a few methods that you can override to listen for objects being passed between the ViewModels. See the following example:
```
using EZShell;

public class SampleViewModel : EZShellViewModel
{
	public SampleViewModel()
    { }
    
    public override Task InitializeAsync(object parameter)
    {
	    return base.InitializeAsync(parameter);
    }

    public override Task ReverseInitAsync(object parameter)
    {
	    return base.ReverseInitAsync(parameter);
    }
}
```

- Dependency Injection
	- For now, `EZShell` is assuming that you are using the built in .netMaui way of registering your Services on app startup. 
	- If you add your ViewModels to your `ServiceCollection`, the EZShell will be able to resolve those dependencies and inject them into your ViewModels.  (See sample below)
```
var builder = MauiApp.CreateBuilder();
builder.UseMauiApp<App>());

builder.Services.AddScoped<SampleViewModel>();
```
```
public SampleViewModel(ISampleInterface sampleService)
{
}
```
## Navigation
- Navigate with `EZShell` using the extension methods based off of the default Shell Navigation.
```
var someData = new { Value = "Some Text" };
Shell.Current.GoToAsync(nameof(SamplePage), someData);
```

- Here are some samples of using the available extension methods.
```
Shell.Current.GoToAsync(nameof(SamplePage), someData);
Shell.Current.PushModalWithNavigation(new SamplePage(), someData);
Shell.Current.PushMultiStackAsync(new List<ShellNavigationState>{ nameof(SamplePage) }, someData);
Shell.Current.PopAsync();
Shell.Current.PopToRootAsync(someData);
Shell.Current.ChangeTabAsync(1, someData, popToRootFirst: true);
```