using SQLite;

namespace Cet301FinalProject.Data;

public class CargoDb
{
    private SQLiteAsyncConnection _database;
    private async Task InitAsync()
    {
        if (_database is not null)
        {
            return;
            
        }
        
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "cargos.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<CargoItem>();
        
    }
    
    
    public async Task<List<CargoItem>> GetAllAsync()
    {
        await InitAsync();
        return await _database.Table<CargoItem>()
            .OrderByDescending(x => x.ArrivalDate).ToListAsync();
    }
    
    
    public async Task CreateAsync(CargoItem item)
    {
        await InitAsync();
        await _database.InsertAsync(item);
    }


    public async Task UpdateAsync(CargoItem item)
    {
        await InitAsync();
        await _database.UpdateAsync(item);
    }


    public async Task DeleteAsync(CargoItem item)
    {
        await InitAsync();
        await _database.DeleteAsync(item);
    }
}