using EzShell.Maui;
using Sample.ContentPages;

namespace Sample;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();

        // Initialize EzShell
        EzShellNavigation.Initialize(this);
        
        // Register routes
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(ThirdPage), typeof(ThirdPage));
    }
}