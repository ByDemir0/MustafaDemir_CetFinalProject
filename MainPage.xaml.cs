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
    private async void EkleButonu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddCargoPage());
    }
}