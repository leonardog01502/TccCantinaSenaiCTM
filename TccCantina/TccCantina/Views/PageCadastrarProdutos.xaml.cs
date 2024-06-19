using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using TccCantina.Models;
using TccCantina.Services;
using SeuNamespace;
using System.IO;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrarProdutos : ContentPage
    {
        public PageCadastrarProdutos()
        {
            InitializeComponent();
        }

        private async void btnVoltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void FrmImgProduto_Tapped(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Selecionar Foto", "Cancelar", null, "Tirar Foto", "Escolher da Galeria");

            if (action == "Tirar Foto")
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                if (photo != null)
                {
                    imgProduto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
                    lblAdicionarImagem.IsVisible = false;
                }
            }
            else if (action == "Escolher da Galeria")
            {
                var photo = await MediaPicker.PickPhotoAsync();

                if (photo != null)
                {
                    imgProduto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
                    lblAdicionarImagem.IsVisible = false;
                }
            }
        }

        private FileResult fotoSelecionada;

        private async void btnCadastrarProduto_Clicked(object sender, EventArgs e)
        {
            var confirmar = await DisplayAlert("Confirmar cadastro", MontarDetalhesProduto(), "Concluir", "Cancelar");

            if (confirmar)
            {
                byte[] imageData = null;

                if (fotoSelecionada != null)
                {
                    imageData = await ConverterImagemParaBytesAsync(fotoSelecionada);
                }

                Produtos produto = new Produtos
                {
                    Nome = txtNome.Text,
                    Descricao = txtDescricao.Text,
                    Valor = Convert.ToDouble(txtValor.Text),
                    Tipo = pickerTipo.SelectedItem.ToString(),
                    Foto = imageData.Length > 0 ? imageData : null,
                };

                BdCantina.CadastrarProduto(produto);

                await DisplayAlert("Sucesso", "Produto cadastrado com sucesso!", "OK");

                await Navigation.PopAsync();
            }
        }

        private async Task<byte[]> ConverterImagemParaBytesAsync(FileResult fotoSelecionada)
        {
            byte[] imageData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var stream = await fotoSelecionada.OpenReadAsync())
                {
                    await stream.CopyToAsync(ms);
                }
                imageData = ms.ToArray();
            }
            return imageData;
        }

        private string MontarDetalhesProduto()
        {
            StringBuilder detalhes = new StringBuilder();
            detalhes.AppendLine($"Nome: {txtNome.Text}");
            detalhes.AppendLine($"Descrição: {txtDescricao.Text}");
            detalhes.AppendLine($"Valor: {txtValor.Text}");
            detalhes.AppendLine($"Tipo: {pickerTipo.SelectedItem}");

            return detalhes.ToString();
        }
    }
}
