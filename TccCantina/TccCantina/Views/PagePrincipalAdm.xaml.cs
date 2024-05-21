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
    public partial class PagePrincipalAdm : MasterDetailPage
    {
        public PagePrincipalAdm()
        {
            InitializeComponent();
            btnHome_Clicked(new Object(), new EventArgs());
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageAdm());
            IsPresented = false;
        }

        private void btnCadastrar_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageCadastrarProdutos());
            IsPresented = false;
        }

        private void btnSobre_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageSobre());
            IsPresented = false;
        }
    }
}