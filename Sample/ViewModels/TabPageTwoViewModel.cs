namespace Sample.ViewModels;

public class TabPageTwoViewModel : BaseViewModel
{
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
    
    public override Task DataReceivedAsync(object parameter)
    {
        if (parameter is string value)
        {
            ParameterText = value;
        }

        return Task.CompletedTask; 
    }
}