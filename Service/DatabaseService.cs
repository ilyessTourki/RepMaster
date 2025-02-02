using System.Collections.ObjectModel;
using System.Security.Permissions;
using SQLite;
using TrainSheet.Interface;

namespace TrainSheet.Service
{
    public class SQLiteDataAccess<T> where T : class, IPrimaryKey, new()
    {
        private SQLiteAsyncConnection _database;


        public SQLiteDataAccess()
        {
        }
        public void InitializeAsync(string dbPath)
        {
            _database = InitializeDatabaseAsync(dbPath);
        }
        private SQLiteAsyncConnection InitializeDatabaseAsync(string dbPath)
        {
            var password = "MySecurePassword123!";
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Secure password for SQLCipher is not set.");
            }

            var options = new SQLiteConnectionString(dbPath, true, key: password);
            var connection = new SQLiteAsyncConnection(options);
            try
            {
                connection.CreateTableAsync<T>().Wait();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("SQLiteException: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("FileNotFoundException: " + ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("DirectoryNotFoundException: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("UnauthorizedAccessException: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("IOException: " + ex.Message);
            }
            return connection;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _database.Table<T>().ToListAsync();
        }
        public async Task<ObservableCollection<T>> GetAllObservableAsync()
        {
            List<T> items = await _database.Table<T>().ToListAsync();
            return new ObservableCollection<T>(items);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _database.Table<T>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveAsync(T item)
        {
            if (item.ID != 0)
            {
                return await _database.UpdateAsync(item);
            }
            else
            {
                return await _database.InsertAsync(item);
            }
        }
        public Task<int> SavePermissionStateAsync(PermissionState permission)
        {
            List<PermissionState> l = new List<PermissionState>();
            l = _database.Table<PermissionState>().ToListAsync().Result;
            if (l.Count == 3)
            {
                return _database.UpdateAsync(permission);
            }
            else
            {
                return _database.InsertAsync(permission);
            }
        }
        public async Task<int> DeleteAllAsync()
        {
            return await _database.DeleteAllAsync<T>();
        }
        public async Task<int> DeleteAsync(T item)
        {
            return await _database.DeleteAsync(item);
        }
        public void ResetAutoIncrement()
        {
            //When deleting items and reinsert (e.g in programs table)
            //they shoud have the same id as before for Statistics
            //to do that you need to Drop and Recreate Table
            _database.DropTableAsync<T>().Wait();
            _database.CreateTableAsync<T>().Wait();
        }

    }
}
