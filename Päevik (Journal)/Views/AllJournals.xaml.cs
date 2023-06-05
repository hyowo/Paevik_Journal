using Journal.ViewModels;

namespace Journal.Views;

public partial class AllJournals : ContentPage
{
	public AllJournals()
	{
		InitializeComponent();
		BindingContext = new JournalListViewModel();
	}

    public async Task ShowToast(string message, int durationInSeconds = 3)
    {
        ToastMessage.Text = message;
        ToastContainer.IsVisible = true;
        await ToastContainer.FadeTo(1, 250);
        await Task.Delay(durationInSeconds * 1000);
        await ToastContainer.FadeTo(0, 250);
        ToastContainer.IsVisible = false;
    }
}