using Journal.Models;
using SQLite;

namespace Journal
{
    public static class Constants
    {
        public const string databaseFileName = "journalsqlite.db3";
        public const SQLite.SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, databaseFileName);
    }
    public class JournalDatabase
    {
        SQLiteAsyncConnection database;

        public JournalDatabase() { }

        async Task Init()
        {
            if (database != null)
                return;

            if (!File.Exists(Constants.DatabasePath))
                CreateDatabase();
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.flags);
            _ = await database.CreateTableAsync<JournalModel>();
        }

        void CreateDatabase()
        {
            EnsureDirectoryExists();
            using (File.Create(Constants.DatabasePath)) { }
        }

        static void EnsureDirectoryExists()
        {
            string directory = Path.GetDirectoryName(Constants.DatabasePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public async Task<List<JournalModel>> GetItemsAsync()
        {
            await Init();
            return await database.Table<JournalModel>().ToListAsync();
        }

        public async Task<JournalModel> GetItemAsync(int id)
        {
            await Init();
            return await database.Table<JournalModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(JournalModel item)
        {
            await Init();
            if (item.Id != 0)
                return await database.UpdateAsync(item);
            else
                return await database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(JournalModel item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }
    }
}
