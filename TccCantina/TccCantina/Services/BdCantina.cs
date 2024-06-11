﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MySqlConnector;
using TccCantina.Models;
using Xamarin.Forms;

namespace TccCantina.Services
{
    public class BdCantina
    {
        static string conn = @"Host=sql.freedb.tech;Port=3306;Database=freedb_TccCantinaSenai;User ID=freedb_TccCantinaSenai;Password=k66@f!ge$CD#qZV;Charset=utf8;";

        public static string StatusMessage { get; set; }

        public static void CadastrarUsuario(ModCantina Usuario)
        {
            try
            {
                string query = "INSERT INTO Usuarios (Nome, CPF, Email, Senha, Matricula, Curso) VALUES (@Nome, @Cpf, @Email, @Senha, @Matricula, @Curso)";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", Usuario.Nome);
                        cmd.Parameters.AddWithValue("@Cpf", Usuario.Cpf);
                        cmd.Parameters.AddWithValue("@Email", Usuario.Email);
                        cmd.Parameters.AddWithValue("@Senha", Usuario.Senha);
                        cmd.Parameters.AddWithValue("@Matricula", Usuario.Matricula);
                        cmd.Parameters.AddWithValue("@Curso", Usuario.Curso);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                StatusMessage = $"Usuário '{Usuario.Nome}' cadastrado com sucesso!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro ao cadastrar usuário: {ex.Message}";
            }
        }

        public static bool LocalizarLogin(string email, string senha)
        {
            try
            {
                string query = "SELECT 1 FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            return reader.Read();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao localizar usuário: {ex.Message}");
            }
        }

        public static void InformacoesUsuario(string email, string senha)
        {
            try
            {
                string query = "SELECT * FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ModCantina modCantina = new ModCantina();
                                modCantina.Nome = reader.GetString("Nome");
                                modCantina.Cpf = reader.GetString("Cpf");
                                modCantina.Email = reader.GetString("Email");
                                modCantina.Senha = reader.GetString("Senha");
                                modCantina.Matricula = reader.GetInt32("Matricula");
                                modCantina.Curso = reader.GetString("Curso");
                                modCantina.Tipo = reader.GetString("Tipo");
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao localizar usuário: {ex.Message}");
            }
        }

        public static void AdicionarCarrinho(int ID, int quantidade)
        {
            try
            {
                string query = "SELECT Produtos.*, Usuarios.*, Carrinho.* FROM Produtos, Usuarios, Carrinho WHERE Carrinho.IdUsuario = Usuarios.Id AND Carrinho.IdProdutos = Produtos.Id";
                string query2 = "INSERT INTO Carrinho()";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Carrinho.IdProdutos", ID);
                        cmd.ExecuteNonQuery();
                    }
                    using (var cmd = new MySqlCommand(query2, con))
                    {
                        cmd.Parameters.AddWithValue("@IdProdutos", ID);
                        cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                        cmd.Parameters.AddWithValue("@IdUsuario", 1);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                //StatusMessage = $"'{nome}' adicionado ao carrinho!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro ao adicionar no carrinho";
            }
        }

        public static void ListarCarrinho(string nome)
        {
            try
            {
                string query = "SELECT * FROM Carrinho (Nome) VALUES (@Nome)";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro ao adicionar no carrinho";
            }
        }

        public static List<CarrinhoFiltrado> ListarCarrinho(int id)
        {
            List<CarrinhoFiltrado> listacarrinho = new List<CarrinhoFiltrado>();
            string sql1 = "SELECT Carrinho.*,Produtos.* from Carrinho,Produtos Where Carrinho.IdUsuario = @Id";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (var cmd = new MySqlCommand(sql1, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CarrinhoFiltrado carrinhoFiltrado = new CarrinhoFiltrado();
                            carrinhoFiltrado.Id = reader.GetInt32("Id");
                            carrinhoFiltrado.Nome = reader.GetString("Nome");
                            carrinhoFiltrado.Quantidade = reader.GetInt32("Quantidade");
                            carrinhoFiltrado.Valor = reader.GetDecimal("Valor");

                            listacarrinho.Add(carrinhoFiltrado);
                        };
                    }
                }

                con.Close();
                return listacarrinho;
            }
        }
        public static List<TotalCarrinho> ValorCarrinho(int Id)
        {
            List<TotalCarrinho> totalcarrinho = new List<TotalCarrinho>();
            string query = "SELECT SUM(Produtos.Valor * Carrinho.Quantidade) AS Total FROM Carrinho INNER JOIN Produtos ON Carrinho.IdProdutos = Produtos.Id WHERE Carrinho.IdUsuario = @Id;";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TotalCarrinho carrinhoTotal = new TotalCarrinho();
                            carrinhoTotal.ValorTotal = reader.GetDecimal("Total");

                            totalcarrinho.Add(carrinhoTotal);
                        };
                    }
                }
                con.Close();
            }
            return totalcarrinho;
        }

        public static void CadastrarProduto(Produtos produto)
        {
            try
            {
                string query = "INSERT INTO Produtos (Nome, Descricao, Valor, Tipo, Foto) VALUES (@Nome, @Descricao, @Valor, @Tipo, @Foto)";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                        cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                        cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                        cmd.Parameters.AddWithValue("@Tipo", produto.Tipo);
                        cmd.Parameters.AddWithValue("@Foto", produto.Foto);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                StatusMessage = $"Produto '{produto.Nome}' cadastrado com sucesso!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro ao cadastrar produto: {ex.Message}";
            }
        }

        public static void EditarProduto(Produtos produto)
        {
            try
            {
                string query = "UPDATE Produtos SET Nome = @Nome, Descricao = @Descricao, Valor = @Valor, Tipo = @Tipo, Foto = @Foto WHERE Id = @Id";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                        cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                        cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                        cmd.Parameters.AddWithValue("@Tipo", produto.Tipo);
                        cmd.Parameters.AddWithValue("@Foto", produto.Foto);
                        cmd.Parameters.AddWithValue("@Id", produto.Id);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                StatusMessage = $"Produto '{produto.Nome}' atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro ao atualizar produto: {ex.Message}";
            }
        }

        public static void ExcluirProduto(int id)
        {
            try
            {
                string query = "DELETE FROM Produtos WHERE Id = @Id";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                StatusMessage = "Produto excluído com sucesso!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro ao excluir produto: {ex.Message}";
            }
        }

        public static Produtos LocalizarProduto(int id)
        {
            Produtos produto = new Produtos();
            try
            {
                string query = "SELECT * FROM Produtos WHERE Id = @Id";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                produto.Id = Convert.ToInt32(reader["Id"]);
                                produto.Nome = reader["Nome"].ToString();
                                produto.Descricao = reader["Descricao"].ToString();
                                produto.Valor = Convert.ToDecimal(reader["Valor"]);
                                produto.Tipo = reader["Tipo"].ToString();
                                produto.Foto = (byte[])reader["Foto"];
                            }
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro ao localizar produto: {ex.Message}";
            }
            return produto;
        }

        public static List<Produtos> ListarProdutos()
        {
            List<Produtos> listaProdutos = new List<Produtos>();
            string query = "SELECT * FROM Produtos";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Produtos produto = new Produtos();
                            produto.Id = reader.GetInt32("Id");
                            produto.Nome = reader.GetString("Nome");
                            produto.Descricao = reader.GetString("Descricao");
                            produto.Valor = reader.GetDecimal("Valor");
                            produto.Tipo = reader.GetString("Tipo");
                            produto.Foto = (byte[])reader["Foto"];

                            listaProdutos.Add(produto);
                        }
                    }
                }
                con.Close();
            }

            return listaProdutos;
        }
        public static string nomePessoa(int ID)
        {
            string nome = null;
            string query = "SELECT Nome FROM Usuarios WHERE Id = @Id";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nome = reader.GetString("Nome");
                        }
                    }
                }
                con.Close();
            }
            return nome;
        }
      
        public static string GerarSenhas()
        {
            int Tamanho = 7; // Numero de digitos da senha
            string senha = string.Empty;
            for (int i = 0; i < Tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());

                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string _char = ((char)codigo).ToString();
                    if (!senha.Contains(_char))
                    {
                        senha += _char;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
            return senha;
        }
    }
}