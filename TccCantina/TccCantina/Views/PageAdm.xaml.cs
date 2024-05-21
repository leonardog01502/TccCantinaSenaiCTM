using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAdm : ContentPage
    {
        public PageAdm()
        {
            InitializeComponent();
        }

        private void imgAnotacaoPrincipal_Tapped(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageAnotacaoPrincipal());
            p.IsPresented = false;
        }

        private void imgGraficoPrincipal_Tapped(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageGraficoPrincipal());
            p.IsPresented = false;
        }
    }
}