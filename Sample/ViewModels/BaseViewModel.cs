using System.ComponentModel;
using System.Runtime.CompilerServices;
using EzShell.Maui;

namespace Sample.ViewModels;

public class BaseViewModel : EzShellViewModel, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}