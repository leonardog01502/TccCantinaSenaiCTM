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
            Reset();
            //indexes = new List<int>();
            //currentIndexes = new List<int>();
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
                var popUpContentView = new PagePopUp(produtoSelecionado.Id);
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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Reset();
            Label clicked = sender as Label;
            ChangeTextColor(Convert.ToInt32(clicked.StyleId.Substring(4, 1)), Color.Yellow);
        }

        void Reset()
        {
            ChangeTextColor(5, Color.Gray);
        }

        void ChangeTextColor(int starcount, Color color)
        {
            //for (int i = 1; i <= starcount; i++)
            //{
            //    (FindByName($"star{i}") as Label).TextColor = color;
            //    //star = Convert.ToInt32(i);
            //}
        }
    }
}
