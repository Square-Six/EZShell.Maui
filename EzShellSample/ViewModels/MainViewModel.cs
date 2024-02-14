using System.Windows.Input;
using Sample.ContentPages;
using EzShell.Maui;
using Sample.Interfaces;

namespace Sample.ViewModels;

public class MainViewModel : BaseViewModel
{
    public ICommand DetailsCommand { get; set; }
    public ICommand DefaultCommand { get; set; }
    public ICommand ChangeTabCommand { get; set; }
    public ICommand ChangeTabParamCommand { get; set; }
    public ICommand PushMultiCommand { get; set; }
    public ICommand PushModalCommand { get; set; }
    
    private string _dIText;
    public string DiText
    {
        get => _dIText;
        set
        {
            _dIText = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel(ISampleService sampleService)
    {
        DefaultCommand = new Command(DefaultPush);
        DetailsCommand = new Command(OnDetails);
        ChangeTabCommand = new Command(ChangeTab);
        ChangeTabParamCommand = new Command(ChangeTabWithParameters);
        PushMultiCommand = new Command(PushMultiViews);
        PushModalCommand = new Command(PushModal);

        DiText = sampleService.Test();
    }
    
    protected override void OnAppearing()
    {
        Console.WriteLine("OnAppearing");
    }

    public override Task ReverseDataReceivedAsync(object? parameter)
    {
        if (parameter is string value)
        {
            return Application.Current.MainPage.DisplayAlert("Success", value, "OK");
        }

        return Task.CompletedTask;
    }
    
    private void PushModal()
    {
        Shell.Current.PushModalAsync(new ThirdPage(), "Sample Modal data");
    }

    private void PushMultiViews()
    {
        var pages = new List<ShellNavigationState>
        {
            nameof(DetailsPage),
            nameof(ThirdPage)
        };
        // Shell.Current.PushMultiStackAsync(pages);
        Shell.Current.PushMultiStackAsync(pages, "With Data");
    }

    private void DefaultPush()
    {
        Shell.Current.PushAsync(nameof(DetailsPage));
    }

    private void OnDetails()
    {
        Shell.Current.PushAsync(nameof(DetailsPage), "Data sent from Main Page!");
    }

    private void ChangeTab()
    {
        Shell.Current.ChangeTabAsync(1);
    }
    
    private void ChangeTabWithParameters()
    {
        Shell.Current.ChangeTabAsync(1, "Tab changed with Parameter");
    }
}