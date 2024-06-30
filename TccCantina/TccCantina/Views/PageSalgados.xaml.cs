using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TccCantina.Models;
using TccCantina.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSalgados : ContentPage
    {

        public PageSalgados()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var Produtos = BdCantina.ListarProdutos();
            if (Produtos != null)
            {
                lsvProdutos.ItemsSource = Produtos;

            }
        }
        
        private async void lsvProdutos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Produtos produtoSelecionado)
            {
                var popUpContentView = new PagePopUp(produtoSelecionado.IdProdutos);
                var popupPage = new ContentPage
                {
                    Content = popUpContentView,
                    BackgroundColor = new Color(0, 0, 0, 0.5)
                };

                await Navigation.PushModalAsync(popupPage);
            }
        }

        private async void btnVoltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
