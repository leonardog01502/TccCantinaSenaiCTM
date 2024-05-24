using System;
using TccCantina.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new PageAdm());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
