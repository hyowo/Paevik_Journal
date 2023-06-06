using Journal.Models;
using Journal.ViewModels;

namespace Journal.Views;

public partial class YourJournals : ContentPage
{
	public YourJournals(JournalDatabase database)
	{
		InitializeComponent();
		BindingContext = new JournalListViewModel(database, Preferences.Get("username", ""));
	}

    private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		bool result = await Application.Current.MainPage.DisplayAlert("Deletion", $"Do you want to delete the post titled '{(e.Item as JournalModel).Title}'?", "Yes", "No");
		if (result)
		{
			(BindingContext as JournalListViewModel).DeleteJournal(e.Item as JournalModel);
            await ShowToast("Post deleted sucessfully.");
		}
    }

    private async Task ShowToast(string message, int durationInSeconds = 3)
    {
        ToastMessage.Text = message;
        ToastContainer.IsVisible = true;
        await ToastContainer.FadeTo(1, 250);
        await Task.Delay(durationInSeconds * 1000);
        await ToastContainer.FadeTo(0, 250);
        ToastContainer.IsVisible = false;
    }
}