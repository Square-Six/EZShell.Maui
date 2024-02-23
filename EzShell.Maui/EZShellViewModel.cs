using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EzShell.Maui;

public partial class EzShellViewModel : ObservableObject,  IEzShellViewModel
{
    public ICommand OnAppearingCommand { get; }
    public ICommand OnDisAppearingCommand { get; }

    protected EzShellViewModel()
    {
        OnAppearingCommand = new Command(ViewAppearing);
        OnDisAppearingCommand = new Command(ViewDisappearing);
    }

    private void ViewAppearing()
    {
        Task.Run(async () => {
            await OnAppearing();
        });
    }

    private void ViewDisappearing()
    {
        Task.Run(async () => {
            await OnDisAppearing();
        });
    }

    public virtual Task OnAppearing() => Task.CompletedTask;
    
    public virtual Task OnDisAppearing() => Task.CompletedTask;
    
    public virtual Task DataReceivedAsync(object? parameter) => Task.CompletedTask;

    public virtual Task ReverseDataReceivedAsync(object? parameter) => Task.CompletedTask;
}