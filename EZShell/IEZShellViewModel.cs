namespace EZShell;

public interface IEzShellViewModel
{
    object Parameter { get; set; }
    object ReversParameter { get; set; }
    Task InitializeAsync();
    Task InitializeAsync(object parameter);
    Task ReverseInitAsync(object parameter);
    void SetParameter(object parameter);
    void SetReverseParameter(object parameter);
}