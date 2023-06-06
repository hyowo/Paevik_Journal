using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Journal.Models;

namespace Journal.ViewModels
{
    public partial class MyJournalsListViewModel : ObservableObject
    {
        private readonly JournalDatabase database;
        private ObservableCollection<JournalModel> journals;
        public ObservableCollection<JournalModel> Journals
        {
            get { return journals; }
            set { SetProperty(ref journals, value); }
        }
        public MyJournalsListViewModel(JournalDatabase journalDatabase)
        {
            database = journalDatabase;
            Journals = new();
            RefreshJournals();
        }

        [RelayCommand]
        public async void RefreshJournals()
        {
            Journals.Clear();
            if (Preferences.ContainsKey("username"))
            {
                var journalModels = (await database.GetItemsAsync()).Where(j => j.OriginalPoster == Preferences.Get("username", string.Empty));
                foreach (var j in journalModels)
                    Journals.Add(j);
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
            RefreshJournals();
        }
    }
}