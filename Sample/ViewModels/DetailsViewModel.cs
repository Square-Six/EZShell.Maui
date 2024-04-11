using System.Windows.Input;
using EzShell.Maui;

namespace Sample.ViewModels;

public class DetailsViewModel : BaseViewModel
{
    public ICommand PopCommand { get; set; }
    public ICommand PopParamCommand { get; set; }
    
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

    public DetailsViewModel()
    {
        PopCommand = new Command(PopView);
        PopParamCommand = new Command(PopViewWithParameters);
    }

    protected override void OnAppearing()
    {
        Console.WriteLine("OnAppearing");
    }

    public override Task DataReceivedAsync(object parameter)
    {
        if (parameter is string value)
            ParameterText = value;
        else
        {
            ParameterText = "Default Push";
        }
        
        return Task.CompletedTask; 
    }
    
    private void PopView()
    {
        Shell.Current.PopAsync();
    }
    
    private void PopViewWithParameters()
    {
        Shell.Current.PopAsync("View popped with Parameters");
    }
}