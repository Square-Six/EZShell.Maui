namespace EzShell.Maui;

/// <summary>
/// Contains extension methods for configuring EzShell in a Maui app.
/// </summary>
public static class EzShellMauiBuilderExtensions
{
    /// <summary>
    /// Represents the service provider used for dependency injection in EzShell framework.
    /// </summary>
    public static IServiceProvider? ServiceProvider { get; private set; }

    /// <summary>
    /// Uses EzShell to configure the EzShellMauiBuilderExtensions in a Maui app.
    /// </summary>
    /// <param name="builder">The MauiAppBuilder instance.</param>
    /// <returns>The modified MauiAppBuilder instance.</returns>
    public static MauiAppBuilder UseEzShell(this MauiAppBuilder builder)
    {
        ServiceProvider = builder.Services.BuildServiceProvider();
        return builder;
    }
}