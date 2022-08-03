namespace EZShell;

public class IOCHelper
{
    public static object GetService(Type type) => AppServices.GetService(type);

    public static IServiceProvider AppServices =>
#if WINDOWS10_0_17763_0_OR_GREATER
			MauiWinUIApplication.Current.Services;
#elif ANDROID
                MauiApplication.Current.Services;
#elif IOS || MACCATALYST
			MauiUIApplicationDelegate.Current.Services;
#else
        null;
#endif
}
