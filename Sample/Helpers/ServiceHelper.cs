using System;

namespace EZShellSample.Helpers
{
    public static class ServiceHelper
    {
        public static TService GetService<TService>() => AppServices.GetService<TService>();

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
}

