using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TccCantina.Services;
using Xamarin.Essentials;
using System.Security.Cryptography;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLogin : ContentPage
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private async void btnEntrar_Clicked(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;
            int Id;

            if (email == "Admin" && senha == "Admin")
            {
                PageAdm adm = new PageAdm();
                await Navigation.PushAsync(adm);
            }
            else
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                {
                    await DisplayAlert("Erro", "Por favor, digite o email e a senha.", "OK");
                    return;
                }

                try
                {
                    int id = BdCantina.LocalizarLogin(email, senha);

                    if (id > 0)
                    {
                        await SecureStorage.SetAsync("@Email", email);
                        await SecureStorage.SetAsync("@Senha", senha);

                        BdCantina.InformacoesUsuario(email, senha);
                        await Navigation.PushAsync(new PageHome(id));
                        Navigation.RemovePage(this);
                    }
                    else
                    {
                        lblEmailError.Text = "Email ou Senha incorretos";
                        lblEmailError.TextColor = Color.Red;
                        lblSenhaError.TextColor = Color.Red;

                        await DisplayAlert("Aviso", "Faça um cadastro para você entrar em nosso Aplicativo, Obrigado ;)", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", $"Ocorreu um erro ao fazer login: {ex.Message}", "OK");
                }
            }
        }

        private async void btnCadastro_Clicked(object sender, EventArgs e)
        {
            PageCadastro Cadastro = new PageCadastro();
            await Navigation.PushAsync(Cadastro);
        }
    }
}