# EZShell.Maui

EZShell.Maui allows more complex transfer of data between ViewModels using Xamarin Shell Navigation as well as Dependency Injection.


## Setup

- In your AppShell.cs class, add the following line of code.
```
SquareSixCore.Init();
```
    - Example (ie)
    
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
