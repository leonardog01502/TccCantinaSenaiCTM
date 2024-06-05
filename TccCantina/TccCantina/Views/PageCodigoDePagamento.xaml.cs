using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TccCantina.Services;

namespace TccCantina.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageCodigoDePagamento : ContentPage
	{
		public PageCodigoDePagamento ()
		{
			InitializeComponent ();
            string senha = BdCantina.GerarSenhas();
            Codigo.Text = senha.ToUpper();
        }
	}
}