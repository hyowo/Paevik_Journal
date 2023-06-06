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
		}
    }
}