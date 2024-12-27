namespace Sukrucan_Alkan_21010310019_proje2;

public partial class Haberler_Detaylar : ContentPage
{
    Item haber;

    public Haberler_Detaylar(Item item)
    {
        InitializeComponent();
        haber = item;
        LoadWebView();
    }

    private void LoadWebView()
    {
        if (!string.IsNullOrEmpty(haber?.link))
        {
            webView.Source = new Uri(haber.link);
        }
        else
        {
            webView.Source = "about:blank";
        }
    }

    private async void Paylas(object sender, EventArgs e)

    {
        shareButtonFrame.BackgroundColor = Colors.Green;


        await shareUri(haber.link, Share.Default);
    }

    private async Task shareUri(string link, IShare share)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = link
        });
        Title = haber.title;
    }

    private async void Geri(object sender, EventArgs e)
    {
        geriButtonFrame.BackgroundColor = Colors.Red;
        await Navigation.PopModalAsync();
    }


}