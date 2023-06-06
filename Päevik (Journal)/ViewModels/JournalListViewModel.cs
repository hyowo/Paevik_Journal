using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Journal.Models;
using Journal.Views;

namespace Journal.ViewModels
{
    public partial class JournalListViewModel : ObservableObject
    {
        private readonly JournalDatabase database;
        private ObservableCollection<JournalModel> journals;
        public ObservableCollection<JournalModel> Journals
        {
            get { return journals; }
            set { SetProperty(ref journals, value); }
        }

        [ObservableProperty]
        private string username;
        public JournalListViewModel(JournalDatabase journalDatabase, string username = "")
        {
            database = journalDatabase;
            Journals = new();
            Username = username;
            RefreshJournals(this.username);
        }

        [RelayCommand]
        public void NavigateToNewJournal()
        {
            Application.Current.MainPage.Navigation.PushAsync(new NewJournal(database));
        }

        [RelayCommand]
        public void NavigateToMyJournals()
        {
            Application.Current.MainPage.Navigation.PushAsync(new YourJournals(database));
        }

        [RelayCommand]
        public async void RefreshJournals(string username = "")
        {
            if (username.Length > 0)
            {
                var journalModels = (await database.GetItemsAsync()).Where(j => j.OriginalPoster == username);
                Journals = new(journalModels);
            }
            else
            {
                var journalModels = (await database.GetItemsAsync()).Where(j => j.IsPrivate == false).ToList();
                Journals = new(journalModels);
            }
        }

        [RelayCommand]
        public async void DeleteJournal(int id)
        {
            var journal_td = await database.GetItemAsync(id);
            if (journal_td != null)
            {
                await database.DeleteItemAsync(journal_td);
            }
            RefreshJournals(username);
        }
    }
}