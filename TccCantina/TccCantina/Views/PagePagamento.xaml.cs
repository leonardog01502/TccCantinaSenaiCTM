using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePagamento : ContentPage
    {
        public PagePagamento()
        {
            InitializeComponent();
            LoadPaymentMethods();
        }

        private void LoadPaymentMethods()
        {
            var paymentMethods = new List<string>
            {
                "Cartão de Crédito",
                "Cartão de Débito",
                "Dinheiro",
                "Pix"
            };
            paymentPicker.ItemsSource = paymentMethods;
        }

        private void OnPaymentConfirmButtonClicked(object sender, EventArgs e)
        {
            var selectedPaymentMethod = paymentPicker.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedPaymentMethod))
            {
                // Navegar para outra página após a seleção da forma de pagamento
                Navigation.PushAsync(new PageCodigoDePagamento());
            }
            else
            {
                DisplayAlert("Erro", "Por favor, selecione uma forma de pagamento.", "OK");
            }
        }
    }
}
