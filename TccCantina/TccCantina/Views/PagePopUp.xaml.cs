using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccCantina.Models;
using TccCantina.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagePopUp : ContentView
	{
        int ID;

        public PagePopUp(int Id)
        {
            InitializeComponent();
            ID = Id;
        }

        int quantidade = 1;

        private void btnMenos_Clicked(object sender, EventArgs e)
        {
            quantidade = Convert.ToInt32(lblQuantidade.Text);
            quantidade -= 1;
            lblQuantidade.Text = Convert.ToString(quantidade);
        }

        private void btnMais_Clicked(object sender, EventArgs e)
        {
            quantidade = Convert.ToInt32(lblQuantidade.Text);
            quantidade += 1;
            lblQuantidade.Text = Convert.ToString(quantidade);
        }

        private void btnConcluir_Clicked(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(lblQuantidade.Text);

            BdCantina.AdicionarCarrinho(ID, quantidade);

            if (Parent is Page parentPage)
            {
                if (parentPage is NavigationPage navigationPage)
                {
                    navigationPage.Navigation.PopAsync();
                }
                else
                {
                    parentPage.Navigation.PopModalAsync();
                }
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            if (Parent is Page parentPage)
            {
                if (parentPage is NavigationPage navigationPage)
                {
                    navigationPage.Navigation.PopAsync();
                }
                else
                {
                    parentPage.Navigation.PopModalAsync();
                }
            }
        }
    }
}