using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Journal.Models;

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
    }
}