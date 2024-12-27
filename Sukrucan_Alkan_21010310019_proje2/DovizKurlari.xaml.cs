using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sukrucan_Alkan_21010310019_proje2
{
    public partial class DovizKurlari : ContentPage
    {
        public DovizKurlari()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Load();
        }

        AltinDoviz kurlar;

        async Task Load()
        {
            try
            {
                string jsondata = await GetAltinDovizGuncelKurlar();
                kurlar = JsonSerializer.Deserialize<AltinDoviz>(jsondata);

                Sepet.ItemsSource = new List<Doviz>()
                {
                    new Doviz() { doviz_adi = "Dolar", doviz_alis = kurlar.USD.alis, doviz_satis = kurlar.USD.satis, Fark = kurlar.USD.degisim, Yon = GetImage(kurlar.USD.d_yon) },
                    new Doviz() { doviz_adi = "Euro", doviz_alis = kurlar.EUR.alis, doviz_satis = kurlar.EUR.satis, Fark = kurlar.EUR.degisim, Yon = GetImage(kurlar.EUR.d_yon) },
                    new Doviz() { doviz_adi = "Sterlin", doviz_alis = kurlar.GBP.alis, doviz_satis = kurlar.GBP.satis, Fark = kurlar.GBP.degisim, Yon = GetImage(kurlar.GBP.d_yon) },
                };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Veri yüklenirken bir hata oluştu: {ex.Message}", "Tamam");
            }
        }

        private string GetImage(string str)
        {
            if (str.Contains("up"))
                return "up.png";
            if (str.Contains("down"))
                return "down.png";
            if (str.Contains("minus"))
                return "minus.png";

            return "";
        }

        async Task<string> GetAltinDovizGuncelKurlar()
        {
            string url = "https://api.genelpara.com/embed/altin.json";
            using (HttpClient client = new HttpClient())
            {
                // Kullanıcı ajanı ekleniyor
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"API isteği başarısız oldu: {response.StatusCode}");
                }

                string jsondata = await response.Content.ReadAsStringAsync();
                return jsondata;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Load();
            await Task.Delay(1000);
        }
    }

    public class Doviz
    {
        public string doviz_adi { get; set; }
        public string doviz_alis { get; set; }
        public string doviz_satis { get; set; }
        public string Fark { get; set; }
        public string Yon { get; set; }
    }

    public class AltinDoviz
    {
        public USD USD { get; set; }
        public EUR EUR { get; set; }
        public GBP GBP { get; set; }
    }

    public class USD
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_yon { get; set; }
    }

    public class EUR
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_yon { get; set; }
    }

    public class GBP
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_yon { get; set; }
    }
}
