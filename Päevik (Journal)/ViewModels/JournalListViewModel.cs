using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JournalModel.ViewModels
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

        private IList<JournalModel> GetJournalModels()
        {
            // Code to retrieve the list of JournalModels from your data source
            // You can replace this with your own implementation
            return new List<JournalModel>
            {
                new JournalModel { isPrivate = true, content = "JournalModel content 1", postingDateTime = DateTime.Now, originalPoster = "User 1" },
                new JournalModel { isPrivate = false, content = "JournalModel content 2", postingDateTime = DateTime.Now.AddDays(-1), originalPoster = "User 2" },
                // Add more JournalModel objects as needed
            };
        }

        #region INotifyPropertyChanged

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

        #endregion
    }
}
