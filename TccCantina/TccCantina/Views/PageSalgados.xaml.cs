using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TccCantina.Models;
using TccCantina.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TccCantina.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSalgados : ContentPage
    {
        public int star { get; set; } 
        public string feed { get; set; }

        public List<int> indexes;

        public List<int> currentIndexes;

        public PageSalgados()
        {
            InitializeComponent();
            //Reset();
            indexes = new List<int>();
            currentIndexes = new List<int>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var Produtos = BdCantina.ListarProdutos();
            if (Produtos != null)
            {
                lsvProdutos.ItemsSource = Produtos;
            }
        }
        
        private async void lsvProdutos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Produtos produtoSelecionado)
            {
                var popUpContentView = new PagePopUp(produtoSelecionado.IdProdutos);
                var popupPage = new ContentPage
                {
                    Content = popUpContentView,
                    BackgroundColor = new Color(0, 0, 0, 0.5)
                };

                await Navigation.PushModalAsync(popupPage);
            }
        }

        private async void btnVoltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    Reset();
        //    Label clicked = sender as Label;
        //    ChangeTextColor(Convert.ToInt32(clicked.StyleId.Substring(4, 1)), Color.Yellow);
        //}

        //void Reset()
        //{
        //    ChangeTextColor(5, Color.Gray);
        //}

        //void ChangeTextColor(int starcount, Color color)
        //{
        //    for (int i = 1; i <= starcount; i++)
        //    {
        //        (FindByName($"star{i}") as Label).TextColor = color;
        //        star = Convert.ToInt32(i);
        //    }
        //}

        private void edtFeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            //errorMessage.IsVisible = false;
            //Editor c = ((Editor)sender);
            //int numOfNextLines = (c.Text).Split('\n').Length;
            //string text = c.Text;
            //textCounter.Text = text.Length.ToString();

            //if (numOfNextLines < 5)
            //{
            //    string addedText = text.Split('\n').Last();
            //    if (addedText.Length > 35)
            //    {
            //        if (string.IsNullOrWhiteSpace((text.Last()).ToString()))
            //        {
            //            if (numOfNextLines < 4)
            //                c.Text += "\n";
            //            else
            //            {
            //                c.Text = e.OldTextValue;
            //                errorMessage.Text = "Reached 4 lines";
            //                errorMessage.IsVisible = true;
            //            }
            //        }
            //        else
            //        {
            //            currentIndexes.Add(c.Text.Length - 1);
            //            int lastIdx = text.LastIndexOf(" ");
            //            if (lastIdx != -1)
            //            {
            //                text = text.Remove(lastIdx, 1);
            //                c.Text = text.Insert(lastIdx, "\n");
            //                indexes.Add(lastIdx);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (e.NewTextValue?.Length < e.OldTextValue?.Length && currentIndexes.Contains(c.Text.Length))
            //        {
            //            if (indexes.Contains(c.Text.LastIndexOf("\n")))
            //            {
            //                int removeIdx = c.Text.LastIndexOf("\n");
            //                if (removeIdx != -1)
            //                {
            //                    text = text.Remove(removeIdx, 1);
            //                    c.Text = text.Insert(removeIdx, " ");
            //                    indexes.Remove(removeIdx);
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    c.Text = e.OldTextValue;
            //    errorMessage.Text = "Ultrapassou as 4 linhas.";
            //    errorMessage.IsVisible = true;
            //}
        }

        private void Enviar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string con = @"Host=sql.freedb.tech;Port=3306;Database=freedb_TccCantinaSenai;User ID=freedb_TccCantinaSenai;Password=k66@f!ge$CD#qZV;Charset=utf8;";
                string sql = "INSERT INTO Feedback(Feed,Estrelas) VALUES (@Feed, @Estrelas)";
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@Feed", MySqlDbType.VarChar).Value = feed;
                        cmd.Parameters.Add("@Estrelas", MySqlDbType.Int32).Value = star;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    DisplayAlert("Obrigado!", "Feedback, enviado com sucesso!", "OK");
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Erro", er.Message, "OK");
            }
        }
    }
}
