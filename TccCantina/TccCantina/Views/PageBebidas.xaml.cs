using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBebidas : ContentPage
    {
        public PageBebidas()
        {
            InitializeComponent();
        }

        private void lsvProdutos_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void btnVoltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}