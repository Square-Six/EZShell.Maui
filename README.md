# EZShell.Maui

EZShell.Maui allows more complex transfer of data between ViewModels using Xamarin Shell Navigation as well as Dependency Injection.


## Setup

- In your AppShell.cs class, add the following line of code.
```
EZShellNavigation.Initialize(this);
```
    Example:
    
    public partial class AppShell : Shell
    {
	    public AppShell()
	    {
		    InitializeComponent();

            EZShellNavigation.Initialize(this);
        }
    }
    


## Usage
```

```
