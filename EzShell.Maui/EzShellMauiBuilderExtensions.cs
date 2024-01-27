namespace EzShell.Maui;

public static class EzShellMauiBuilderExtensions
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    public static MauiAppBuilder UseEzShell(this MauiAppBuilder builder)
    {
        ServiceProvider = builder.Services.BuildServiceProvider();
        return builder;
    }
}