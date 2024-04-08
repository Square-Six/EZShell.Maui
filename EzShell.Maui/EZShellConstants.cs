namespace EzShell.Maui;

/// <summary>
/// Constants for the EzShell library.
/// </summary>
public static class EzShellConstants
{
    /// <summary>
    /// Text returned when the Shell instance is not found.
    /// </summary>
    /// <value>
    /// The default text is "An error occurred trying to navigate with Shell navigation".
    /// </value>
    public static string ShellNotFoundText => "An error occurred trying to navigate with Shell navigation";

    /// <summary>
    /// Represents the exception text that is thrown when a ContentPage is found to be null.
    /// </summary>
    public static string NullContentPageExceptionText => "ContentPage was found Null.";

    /// <summary>
    /// Error message for when the expected list of Navigation States is null.
    /// </summary>
    public static string NavigationStatesExceptionText => "Expected List of Navigation States was null. Please pass in the List of ShellNavigationState's (strings) you want to use.";
}