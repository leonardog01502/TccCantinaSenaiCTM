using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TccCantina.Services;
using TccCantina.Models;
using System.Text.RegularExpressions;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastro : ContentPage
    {
        public PageCadastro()
        {
            InitializeComponent();
        }

        private async void btnCadastrar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNome.Text) && !string.IsNullOrEmpty(txtCPF.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtSenha.Text) && !string.IsNullOrEmpty(txtMatricula.Text) && cbxCursos.SelectedItem != null)
            {
                ModCantina novoUsuario = new ModCantina
                {
                    Nome = txtNome.Text,
                    Cpf = txtCPF.Text,
                    Email = txtEmail.Text,
                    Senha = txtSenha.Text,
                    Matricula = Convert.ToInt32(txtMatricula.Text),
                    Curso = cbxCursos.SelectedItem.ToString()
                };    
                
                BdCantina.CadastrarUsuario(novoUsuario);
                await DisplayAlert("Cadastro", "Usuário cadastrado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alerta", "Por favor, preencha todos os campos obrigatórios.", "OK");
            }
        }

        private void txtCPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            var newText = e.NewTextValue;

            var cleanText = Regex.Replace(newText, "[^0-9]", "");

            if (cleanText.Length == 11)
            {
                var formattedText = string.Format("{0:000\\.000\\.000\\-00}", long.Parse(cleanText));
                entry.Text = formattedText;
            }
            else if (cleanText.Length > 11)
            {
                entry.Text = cleanText.Substring(0, 11);
            }
            else
            {
                entry.Text = newText;
            }
            entry.CursorPosition = entry.Text.Length;
        }
    }
}