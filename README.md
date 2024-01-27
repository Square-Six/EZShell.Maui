
# EZShell.Maui

EZShell.Maui enhances the data transfer experience between ViewModels in Maui applications by leveraging Shell Navigation's powerful capabilities combined with robust Dependency Injection. This integration not only simplifies complex data exchanges but also streamlines the development process, providing a more efficient and seamless navigation framework for developers.

## Setup

### AppShell.cs
In the constructor of your **AppShell.cs** class, insert the following line of code after the **InitializeComponent()** method:

```
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        EzShellNavigation.Initialize(this);
    }
}

```

## MauiProgram.cs
To enable dependency injection, add the following line of code in your **MauiProgram.cs** class after registering all of your app's services and interfaces:

```
builder.UseEzShell();
```

## Usage

### ViewModels
Ensure your viewmodels inherit from the 'EzShellViewModel' class to enable them to listen for navigation events and handle parameter passing:

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

## ContentPage

In your ContentPage .xaml file, specify the ViewModelType and set up listeners for OnAppearing and OnDisappearing events:

```
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:Sample.ViewModels"
             xmlns:ez="clr-namespace:EzShell.Maui;assembly=EzShell.Maui"
             ez:EzShellPageExtensions.OnAppearingCommand="{Binding OnAppearingCommand}"
             ez:EzShellPageExtensions.OnDisappearingCommand="{Binding OnDisAppearingCommand}"
             ez:EzShellPageExtensions.ViewModelType="{x:Type vm:MainViewModel}"
             x:Class="Sample.ContentPages.MainPage">
<!-- Page Content -->
</ContentPage>
```

## Navigation

Utilize EzShell's methods to navigate within your application:

```
Shell.Current.GoToAsync(nameof(DetailsPage), someData);
Shell.Current.PopAsync(someData);
Shell.Current.PopToRootAsync(someData);
Shell.Current.ChangeTabAsync(1, someData);
Shell.Current.PushMultiStackAsync(myListOfPages, someData);
Shell.Current.PushModalAsync(myContentPage, someData);
```