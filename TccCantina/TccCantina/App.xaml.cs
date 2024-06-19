using System;
using TccCantina.Services;
using TccCantina.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new PageLogin());
        }

        protected override async void OnStart()
        {
            string storedEmail = await SecureStorage.GetAsync("@Email");
            string storedSenha = await SecureStorage.GetAsync("@Senha");

            if (!string.IsNullOrEmpty(storedEmail) && !string.IsNullOrEmpty(storedSenha))
            {
                int Id = BdCantina.LocalizarLogin(storedEmail, storedSenha);

                if (Id > 0)
                {
                    BdCantina.InformacoesUsuario(storedEmail, storedSenha);
                    MainPage = new NavigationPage(new PageHome(Id));
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
