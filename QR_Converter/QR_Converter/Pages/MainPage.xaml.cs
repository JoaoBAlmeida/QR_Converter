using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QR_Converter.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PopulatePage();
        }

        private void PopulatePage()
        {
            //Populate picker
            pckr_sys.ItemsSource = new List<string>
            {
                "Rio das Ostras"
            };
            //Default selected
            pckr_sys.SelectedIndex = 0;
        }

        private void BtnGoSys(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Converter(pckr_sys.SelectedItem.ToString()));
        }
    }
}
