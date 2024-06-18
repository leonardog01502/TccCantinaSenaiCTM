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
    public partial class PageAdm : ContentPage
    {
        public PageAdm()
        {
            InitializeComponent();
        }

        private async void imgAnotacaoPrincipal_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageAnotacaoPrincipal());
        }

        private async void imgGraficoPrincipal_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCadastrarProdutos());
        }
    }
}