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
        private bool ascending = true;
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
        public async void DeleteJournal(JournalModel model)
        {
            if (model != null)
            {
                await database.DeleteItemAsync(model);
            }
            RefreshJournals(Username);
        }

        [RelayCommand]
        public async void SortJournals()
        {
            ascending ^= true;
            var journalModels = (await database.GetItemsAsync()).Where(j => j.IsPrivate == false).ToList();
            if (ascending)
            {
                Journals = new(journalModels.OrderBy(j => j.PostedAt));
            }
            else
            {
                Journals = new(journalModels.OrderBy(j => j.PostedAt).Reverse());
            }
        }

        public async void SearchByString(string text)
        {
            var journalModels = (await database.GetItemsAsync()).Where(j => j.OriginalPoster.Contains(text) || j.Title.Contains(text) || j.Content.Contains(text)).ToList();
            Journals = new(journalModels);
        }
    }
}