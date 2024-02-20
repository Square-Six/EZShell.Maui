using Sample.ContentPages;

namespace Sample;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        
        // Register routes
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(ThirdPage), typeof(ThirdPage));
    }
}