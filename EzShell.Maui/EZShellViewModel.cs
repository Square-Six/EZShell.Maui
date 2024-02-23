using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EzShell.Maui;

public partial class EzShellViewModel : ObservableObject,  IEzShellViewModel
{
    public ICommand OnAppearingCommand { get; }
    public ICommand OnDisAppearingCommand { get; }

    protected EzShellViewModel()
    {
        OnAppearingCommand = new Command(OnAppearing);
        OnDisAppearingCommand = new Command(OnDisAppearing);
    }
    
    protected virtual void OnAppearing() { }

    protected virtual void OnDisAppearing() { }
    
    public virtual Task DataReceivedAsync(object? parameter) => Task.CompletedTask;

    public virtual Task ReverseDataReceivedAsync(object? parameter) => Task.CompletedTask;
}