﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccCantina.Models;
using TccCantina.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHome : TabbedPage
    {
        int idUser;
        public PageHome(int id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ModCantina modCantina = new ModCantina();
            lblnome.Text = modCantina.Nome;
            lblcpf.Text = modCantina.Cpf;
            lblemail.Text = modCantina.Email;
            lblmatricula.Text = Convert.ToString(modCantina.Matricula);
            lblcurso.Text = modCantina.Curso;
            lbltipo.Text = modCantina.Tipo;
            idUser = id;
            //txtNomeUser.Text = BdCantina.nomePessoa(id);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var Carrinho = BdCantina.ListarCarrinho(idUser);
            if (Carrinho != null)
            {
                lsvProdutos.ItemsSource = Carrinho;
                
            }
            List<TotalCarrinho> totalCarrinhoList = BdCantina.ValorCarrinho(idUser);
            decimal total = totalCarrinhoList.Sum(tc => tc.ValorTotal);
            txtValorTotal.Text = total.ToString();
        }

        private void imgSalgado_Tapped(object sender, EventArgs e)
        {
            PageSalgados salgados = new PageSalgados();
            Navigation.PushAsync(salgados);
        }

        private void imgDoce_Tapped(object sender, EventArgs e)
        {
            PageDoces doce = new PageDoces();
            Navigation.PushAsync(doce);
        }

        private void imgBebidas_Tapped(object sender, EventArgs e)
        {
            PageBebidas bebidas = new PageBebidas();
            Navigation.PushAsync(bebidas);
        }

        private void imgAlmoco_Tapped(object sender, EventArgs e)
        {
            PageAlmoco almoco = new PageAlmoco();
            Navigation.PushAsync(almoco);
        }

        private void lsvProdutos_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void lsvProdutos_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {

        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("@Email");
            SecureStorage.Remove("@Senha");
            await Navigation.PushAsync(new PageLogin());
        }
    }
}