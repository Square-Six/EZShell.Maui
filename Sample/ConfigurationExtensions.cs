using Sample.Interfaces;

namespace Sample;

public static class ConfigurationExtensions
{
    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<ISampleService, SampleService>();
        
        return builder;
    }
}