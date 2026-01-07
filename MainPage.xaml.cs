using System.Collections.ObjectModel;

namespace Cet301FinalProject;

public partial class MainPage : ContentPage
{
    //List yerine ObservableCollection kullanma fikrini Ai'dan aldım.
    public ObservableCollection<KargoModel> Kargolar { get; set; }

    public MainPage()
    {
        InitializeComponent();
        
        Kargolar = new ObservableCollection<KargoModel>
        {

            new KargoModel { TakipNo = "1", UrunAdi = "Kulaklık", Durum = "Yolda", DurumRenk = "#DBEAFE", YaziRenk = "#1E40AF", OgrenciAdi = "Mustafa Demir" },
            new KargoModel { TakipNo = "2", UrunAdi = "Ayakkabı", Durum = "Teslim Edildi", DurumRenk = "#DCFCE7", YaziRenk = "#166534", OgrenciAdi = "Ali Yılmaz" }
        };

        KargoListesi.ItemsSource = Kargolar;
    }
}


public class KargoModel
{
    public string TakipNo { get; set; }
    public string UrunAdi { get; set; }
    public string Durum { get; set; }
    public string DurumRenk { get; set; } 
    public string YaziRenk { get; set; } 
    public string OgrenciAdi { get; set; } 
}