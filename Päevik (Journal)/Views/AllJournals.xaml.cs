using Journal.ViewModels;

namespace Journal.Views;

public partial class AllJournals : ContentPage
{
	public AllJournals()
	{
		InitializeComponent();
		BindingContext = new JournalListViewModel();
	}
}