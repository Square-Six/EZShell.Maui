using EZShellSample.Interfaces;
using EZShellSample.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EZShellSample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        ConfigureServices(builder.Services);

        return builder.Build();
	}

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ISampleInterface, SampleService>();

        services.AddScoped<AboutViewModel>();
    }
}

