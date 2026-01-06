using SQLite;

namespace Cet301FinalProject.Data;

[Table("CargoItems")]
public class CargoItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string? OwnerName { get; set; }  
    public string? CargoCompany { get; set; }
    
    public DateTime ArrivalDate { get; set; } 
    public bool IsReceived { get; set; }     


    public CargoItem()
    {
        ArrivalDate = DateTime.Now;
        IsReceived = false;     
    }
}