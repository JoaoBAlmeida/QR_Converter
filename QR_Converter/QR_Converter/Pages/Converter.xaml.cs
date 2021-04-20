using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using QR_Converter.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace QR_Converter.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Converter : ContentPage
    {
        ExcelExport ExpExcl = new ExcelExport();
        Code_Converter qr_conv;
        public Converter(string type)
        {
            InitializeComponent();
            Title = type;
            qr_conv = new Code_Converter(type);
        }

        private async void BtnGoToScan(object sender, EventArgs e)
        {
            PermissionStatus Status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (Status == PermissionStatus.Granted)
            {
                ZXingScannerPage ScanPage = new ZXingScannerPage();
                await Navigation.PushAsync(ScanPage);
                ScanPage.OnScanResult += (result) =>
                {
                    ScanPage.IsScanning = false;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                        Ent_Code.Text = result.Text;
                    });
                };
            }
            else
            {
                await DisplayAlert("ERROR", "Permissão para usar câmera não foi garantida", "OK");
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
            }
        }
        
        private async void BtnConvert(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Ent_Code.Text))
            {
                await DisplayAlert("ERROR", "Escaneie ou digite o código a ser convertido", "OK");
            }
            else
            {
                if (string.IsNullOrEmpty(Ent_Conv.Text)) Ent_Conv.Text = qr_conv.ConvertCode(Ent_Code.Text);
                else Ent_Conv.Text += "\n" + qr_conv.ConvertCode(Ent_Code.Text);

                Ent_Code.Text = string.Empty;
            }
        }

        private async void BtnExpo(object sender, EventArgs e)
        {
            bool ansr = await DisplayAlert("AVISO", "Esta ação vai coletar os dados que foram salvos até agora, e movê-los para um arquivo .csv." +
               "\nDeseja continuar?", "SIM", "NÃO");
            if (!ansr) return;
            try
            {
                if (string.IsNullOrEmpty(Ent_Conv.Text)) throw new Exception("Must have a converted code already set");
                string result = await ExpExcl.ExportCSV(Ent_Conv.Text);
                Ent_Conv.Text = string.Empty;
                await DisplayAlert("SUCESSO", result, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRO", ex.Message, "OK");
            }
        }
    }
}