using System.Collections.ObjectModel;


namespace Cet301FinalProject;

public partial class MainPage : ContentPage
{
    private readonly Data.CargoDb _cargoDb;
    public ObservableCollection<Data.CargoItem> Kargolar { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        _cargoDb = new Data.CargoDb();
        KargoListesi.ItemsSource = Kargolar;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //Database'i projeye entegre ederken karşılaştığım sorunu bu kod diziniyle çözüldü.
        // UI'ın tamamen yüklenmesi için kısa bir bekleme ekliyoruz.
        // Bu, veritabanı işlemlerinin UI henüz hazır değilken çalışmasını ve çökmesini engeller.
        await Task.Delay(100); 
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            Kargolar.Clear();
            var items = await _cargoDb.GetAllAsync();
            foreach (var item in items)
            {
                Kargolar.Add(item);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Hata oluştu: {ex}");
            await DisplayAlert("Hata", $"Veriler yüklenirken hata oluştu: {ex.Message}", "Tamam");
        }
    }
    private async void Kargo_Tapped(object sender, TappedEventArgs e)
    {
        // Tıklanan öğeyi al
        var frame = sender as Frame;
        var item = frame?.BindingContext as Data.CargoItem;

        if (item == null) return;

        string action = await DisplayActionSheet("İşlem Seçiniz", "İptal", null, "Teslim Edildi Olarak İşaretle", "Sil");

        if (action == "Sil")
        {
            await _cargoDb.DeleteAsync(item);
            Kargolar.Remove(item);
        }
        else if (action == "Teslim Edildi Olarak İşaretle")
        {
            item.isArrived = true;
            await _cargoDb.UpdateAsync(item);
            
            // Listeyi yenilemek için
            var index = Kargolar.IndexOf(item);
            if (index != -1)
            {
                Kargolar[index] = item; // Bu, UI'ı tetikler
            }
        }
    }

    private async void EkleButonu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddCargoPage());
    }
}