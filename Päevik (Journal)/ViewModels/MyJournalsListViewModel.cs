using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Journal.Models;
using Journal.Views;

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
        public async void RefreshJournals()
        {
            Journals.Clear();
            var journalModels = (await database.GetItemsAsync()).Where(j => j.IsPrivate == false);
            foreach (var j in journalModels)
                Journals.Add(j);
        }
    }
}