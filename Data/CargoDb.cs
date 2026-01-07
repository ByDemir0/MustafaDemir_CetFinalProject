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

        // HATA ÇÖZÜMÜ: Yolu dolaylı yoldan bulmak yerine doğrudan sistem klasörünü alıyoruz.
        var folderPath = FileSystem.AppDataDirectory;
    
        // Klasör yoksa oluştur (Garanti altına alıyoruz)
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var dbPath = Path.Combine(folderPath, "cargos.db3");


        _database = new SQLiteAsyncConnection(dbPath);
        try
        {
            await _database.CreateTableAsync<CargoItem>();
        }
        catch (Exception)
        {   
            //Database'i projeye entegre ederken karşılaştığım sorunu bu kod diziniyle çözüldü.
            // EĞER tablo yapısı değiştiyse (örneğin Primary Key eklendiyse) eski veriyle çakışabilir.
            // Bu durumda tabloyu silip yeniden oluşturuyoruz. 
            await _database.DropTableAsync<CargoItem>();
            await _database.CreateTableAsync<CargoItem>();
        }
    }
    
    
    public async Task<List<CargoItem>> GetAllAsync()
    {
        await InitAsync();
        return await _database.Table<CargoItem>()
            .OrderByDescending(x => x.GelisTarihi).ToListAsync();
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