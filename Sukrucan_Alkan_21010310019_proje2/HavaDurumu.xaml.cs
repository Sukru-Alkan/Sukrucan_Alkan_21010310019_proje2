using System.Collections.ObjectModel;
using System.Text.Json;

namespace Sukrucan_Alkan_21010310019_proje2;

public partial class HavaDurumu : ContentPage
{
    public HavaDurumu()
    {
        InitializeComponent();

        if (File.Exists(dosyaismi))
        {
            string data = File.ReadAllText(dosyaismi);
        }
        Grad.ItemsSource = Sehirler;
    }
    static string dosyaismi = Path.Combine(FileSystem.Current.AppDataDirectory, "hdata.json");
    static ObservableCollection<SehirHavaDurumu> Sehirler = new ObservableCollection<SehirHavaDurumu>();

    private async void add_sehir(Object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("Þehir :", "Þehir ismi giriniz :", "Devam", "Ýptal");
        sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        sehir = sehir.Replace('Ç', 'C');
        sehir = sehir.Replace('Ð', 'G');
        sehir = sehir.Replace('Ý', 'I');
        sehir = sehir.Replace('Ö', 'O');
        sehir = sehir.Replace('Ü', 'U');
        sehir = sehir.Replace('Þ', 'S');

        Sehirler.Add(new SehirHavaDurumu() { Name = sehir });
        string data = JsonSerializer.Serialize(Sehirler);
        File.WriteAllText(dosyaismi, data);

    }

    private async void Sil(object sender, EventArgs e)
    {
        var sil = sender as ImageButton;
        if (sil != null)
        {
            var pop = Sehirler.First(o => o.Name == sil.CommandParameter.ToString());
            Sehirler.Remove(pop);

            // Asenkron olarak dosyayý güncelle
            string data = JsonSerializer.Serialize(Sehirler);
            await File.WriteAllTextAsync(dosyaismi, data);
        }
    }



}
public class SehirHavaDurumu
{
    public string Name { get; set; }
    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
}