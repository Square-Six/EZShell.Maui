namespace EzShell.Maui;

public interface IEzShellViewModel
{
    Task DataReceivedAsync(object? parameter);
    Task ReverseDataReceivedAsync(object? parameter);
}