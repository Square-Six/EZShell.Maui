using System.Windows.Input;
using EzShell.Maui;

namespace Sample.ViewModels;

public class ThirdViewModel : BaseViewModel
{
    public ICommand PopRootCommand { get; set; }
    
    private string _text;
    public string ParameterText
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged();
        }
    }

    public ThirdViewModel()
    {
        PopRootCommand = new Command(PopToRoot);
    }
    
    public override Task DataReceivedAsync(object parameter)
    {
        if (parameter is string value)
            ParameterText = value;
        
        return Task.CompletedTask; 
    }

    private void PopToRoot()
    {
        Shell.Current.PopToRootAsync();
    }
}