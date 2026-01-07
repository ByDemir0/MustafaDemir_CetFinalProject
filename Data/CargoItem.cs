using SQLite;

namespace Cet301FinalProject.Data;

[Table("CargoItems")]
public class CargoItem
{
    [PrimaryKey, AutoIncrement]
    public int TakipNo { get; set; }
    public string? UrunAdi {get; set;}
    public string? OgrenciAdi { get; set; }
    public DateTime GelisTarihi { get; set; } 
    public bool isArrived { get; set; }     


    public CargoItem()
    {
        GelisTarihi = DateTime.Now;
        isArrived = false;     
    }

    
    //Bu kısım Ai yardımıyla yapılmıştır.
    // Veritabanına sütun olarak eklenmeyen fakat görünüş için hesaplanan özellikler.
    [Ignore] 
    public string Tarih => GelisTarihi.ToString("dd/MM/yyyy")  ;


    [Ignore]
    public string Durum => isArrived ? "Teslim Edildi" : "Teslim Alınmadı";

    [Ignore]
    public string DurumRenk => isArrived ? "#DCFCE7" : "#DBEAFE"; 

    [Ignore]
    public string YaziRenk => isArrived ? "#166534" : "#1E40AF";
}