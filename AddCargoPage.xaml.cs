namespace Cet301FinalProject;

public partial class AddCargoPage : ContentPage
{
    private readonly Data.CargoDb _cargoDb;

    public AddCargoPage()
    {
        InitializeComponent();
        _cargoDb = new Data.CargoDb();
    }

    private async void Kaydet_Clicked(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(UrunAdiEntry.Text) || string.IsNullOrWhiteSpace(OgrenciAdiEntry.Text))
        {
            await DisplayAlert("Hata", "Lütfen ürün ve öğrenci adını giriniz.", "Tamam");
            return;
        }


        var yeniKargo = new Data.CargoItem
        {
            UrunAdi = UrunAdiEntry.Text,
            OgrenciAdi = OgrenciAdiEntry.Text,
            
        };


        await _cargoDb.CreateAsync(yeniKargo);


        await Navigation.PopModalAsync();
    }

    private async void Iptal_Clicked(object sender, EventArgs e)
    {

        await Navigation.PopModalAsync();
    }
}