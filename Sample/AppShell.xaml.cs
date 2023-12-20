using EZShell;
using EZShellSample;

namespace Sample;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        
        EzShellNavigation.Initialize(this);

        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(DetailsModalPage), typeof(DetailsModalPage));
        Routing.RegisterRoute(nameof(ThirdDeepPage), typeof(ThirdDeepPage));
        Routing.RegisterRoute(nameof(PopNavTestView), typeof(PopNavTestView));
        Routing.RegisterRoute(nameof(MultiNavFinalPage), typeof(MultiNavFinalPage));
    }
}