using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Journal.Models;
using Journal.Views;

namespace Journal.ViewModels
{
    public partial class JournalListViewModel : ObservableObject
    {
        private ObservableCollection<JournalModel> journals;
        public ObservableCollection<JournalModel> Journals
        {
            get { return journals; }
            set { SetProperty(ref journals, value); }
        }
        public JournalListViewModel()
        {
            Journals = new()
            {
                new JournalModel
                {
                    IsPrivate = false,
                    OriginalPoster = "John Doe",
                    PostedAt = DateTime.Now.AddDays(-2),
                    Title = "First Journal",
                    Content = "This is my first journal entry."
                },
                new JournalModel
                {
                    IsPrivate = true,
                    OriginalPoster = "Jane Smith",
                    PostedAt = DateTime.Now.AddDays(-1),
                    Title = "Private Journal",
                    Content = "This journal entry is private."
                },
                new JournalModel
                {
                    IsPrivate = false,
                    OriginalPoster = "Sam Johnson",
                    PostedAt = DateTime.Now,
                    Title = "New Journal",
                    Content = "Just started a new journal."
                }
            };
        }

        [RelayCommand]
        public void NavigateToNewJournal()
        {
            Application.Current.MainPage.Navigation.PushAsync(new NewJournal());
        }

        [RelayCommand]
        public void NavigateToMyJournals()
        {
            Application.Current.MainPage.Navigation.PushAsync(new YourJournals());
        }
    }
}