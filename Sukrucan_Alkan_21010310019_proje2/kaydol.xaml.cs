
using System;
using Microsoft.Maui.Controls;

namespace Sukrucan_Alkan_21010310019_proje2;


public partial class kaydol : ContentPage
{
    private readonly FirebaseAuthService _authService;

    public kaydol()
    {
        InitializeComponent();
        _authService = new FirebaseAuthService();
    }


    private async void OnLabelTapped(object sender, EventArgs e)
    {
        // Farklý bir sayfaya yönlendirme
        await Navigation.PushAsync(new giris());
    }
    private async void OnRegisterClicked(object sender, EventArgs e)
    {

        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Hata", "E-posta ve þifre boþ býrakýlamaz.", "Tamam");
            return;
        }

        string result = await _authService.RegisterUserAsync(email, password);
        await DisplayAlert("Sonuç", result, "Tamam");
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Hata", "E-posta ve þifre boþ býrakýlamaz.", "Tamam");
            return;
        }

        string result = await _authService.LoginUserAsync(email, password);
        await DisplayAlert("Sonuç", result, "Tamam");
    }
}
