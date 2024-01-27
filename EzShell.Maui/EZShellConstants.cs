namespace EzShell.Maui;

public static class EzShellConstants
{
    public static string NullShellRefExceptionText => "Shell parameter is missing or a Null value. Make sure to initialize EZShellNavigation in the App startup.";
    public static string NullContentPageExceptionText => "ContentPage was found Null.";
    public static string NavigationStatesExceptionText => "Expected List of Navigation States was null. Please pass in the List of ShellNavigationState's (strings) you want to use.";
}