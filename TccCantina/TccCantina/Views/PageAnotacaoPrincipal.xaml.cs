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
    public partial class PageAnotacaoPrincipal : ContentPage
    {
        public PageAnotacaoPrincipal()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lsvClientes.ItemsSource = BdCantina.listarCliente();
        }

        private void imgBtnZap_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var cliente = button?.BindingContext as ModCantina;
            var telefone = cliente?.Telefone;

            if (!string.IsNullOrEmpty(telefone))
            {
                telefone = new string(telefone.Where(char.IsDigit).ToArray());
                if (Device.RuntimePlatform == Device.Android)
                {
                    var mensagem = "Seu Pedido está pronto!!";
                    var whatsappUri = $"https://wa.me/{telefone}?text={Uri.EscapeDataString(mensagem)}";
                }
                else
                {
                    DisplayAlert("Erro", "Esta funcionalidade só está disponível em dispositivos Android.", "OK");
                }
            }
            else
            {
                DisplayAlert("Erro", "O número de telefone não está disponível para este cliente.", "OK");
            }

        }
    }
}