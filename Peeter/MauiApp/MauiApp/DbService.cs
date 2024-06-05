using SQLite;

namespace MauiApp
{
    public class DbService
    {
        private const string DB_NAME = "db";
        private readonly SQLiteAsyncConnection _connection;

        public DbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Item>().Wait(); 
        }

        public async Task<List<Item>> GetItems()
        {
            return await _connection.Table<Item>().ToListAsync();
        }

        public async Task<Item> GetById(int id)
        {
            return await _connection.Table<Item>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Item item)
        {
            await _connection.InsertAsync(item);
        }

        public async Task Update(Item item)
        {
            await _connection.UpdateAsync(item);
        }


    }
}
