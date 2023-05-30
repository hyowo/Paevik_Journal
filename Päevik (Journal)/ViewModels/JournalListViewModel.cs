using Journal.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Journal.ViewModels
{
    public class JournalModelListViewModel : INotifyPropertyChanged
    {
        private IList<JournalModel> _JournalModels;

        public IList<JournalModel> JournalModels
        {
            get { return _JournalModels; }
            set { SetProperty(ref _JournalModels, value); }
        }

        public JournalModelListViewModel()
        {
            JournalModels = GetJournalModels();
        }

        private static IList<JournalModel> GetJournalModels()
        {
            // Code to retrieve the list of JournalModels from your data source
            return new List<JournalModel>
            {
                new JournalModel { IsPrivate = true, Title = "My day at here.", Content = "JournalModel content 1", PostedAt = DateTime.Now, OriginalPoster = "User 1" },
                new JournalModel { IsPrivate = false, Title = "Someone there.", Content = "JournalModel content 2", PostedAt = DateTime.Now.AddDays(-1), OriginalPoster = "User 2" },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
