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
            CheckStoredCredentials();
        }

        private async void CheckStoredCredentials()
        {
            string storedEmail = await SecureStorage.GetAsync("@Email");
            string storedSenha = await SecureStorage.GetAsync("@Senha");
            Console.WriteLine(storedEmail);

            if (!string.IsNullOrEmpty(storedEmail) && !string.IsNullOrEmpty(storedSenha))
            {
                int Id = BdCantina.LocalizarLogin(storedEmail, storedSenha);

                if (Id > 0)
                {
                    BdCantina.InformacoesUsuario(storedEmail, storedSenha);
                    await Navigation.PushAsync(new PageHome(Id));
                    Navigation.RemovePage(this);
                }
            }
        }

        private async void btnEntrar_Clicked(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

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
                    int Id = BdCantina.LocalizarLogin(email, senha);

                    if (Id >= 0)
                    {
                        await SecureStorage.SetAsync("@Email", email);
                        await SecureStorage.SetAsync("@Senha", senha);

                        BdCantina.InformacoesUsuario(email, senha);
                        await Navigation.PushAsync(new PageHome(Id));
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
    