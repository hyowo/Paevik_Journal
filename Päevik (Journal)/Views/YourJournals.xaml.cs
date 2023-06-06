using Journal.ViewModels;

namespace Journal.Views;

public partial class YourJournals : ContentPage
{
	public YourJournals(JournalDatabase database)
	{
		InitializeComponent();
		BindingContext = new JournalListViewModel(database, Preferences.Get("username", ""));
	}
}