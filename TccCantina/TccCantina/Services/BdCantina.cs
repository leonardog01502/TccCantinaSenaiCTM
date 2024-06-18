﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MySqlConnector;
using TccCantina.Models;

namespace TccCantina.Services
{
    public class BdCantina
    {
        static string conn = @"Host=sql.freedb.tech;Port=3306;Database=freedb_TCCcantina;User ID=freedb_TccCantina;Password=Xtt2c7snp!bh6NA;Charset=utf8;";
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
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        return count > 0;
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

        public static void AdicionarCarrinho(int IDCarrinho, int quantidade)
        {
            try
            {
                string query = "SELECT Produtos.*, Usuarios.*, Carrinho.* FROM Produtos, Usuarios, Carrinho WHERE Carrinho.IdUsuario = Usuarios.IdUsuario AND Carrinho.IdProdutos = Produtos.IdProdutos";
                string query2 = "INSERT INTO Carrinho()";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Carrinho.IdProdutos", IDCarrinho);
                        cmd.ExecuteNonQuery();
                    }
                    using (var cmd = new MySqlCommand(query2, con))
                    {
                        cmd.Parameters.AddWithValue("@IdProdutos", IDCarrinho);
                        cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                        cmd.Parameters.AddWithValue("@IdUsuario", IDCarrinho);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                StatusMessage = $"Produto adicionado ao carrinho!";
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

        public static List<Carrinho> ListarCarrinho()
        {
            List<Carrinho> listacarrinho = new List<Carrinho>();
            string sql = "SELECT * FROM Produtos";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Carrinho carrinho = new Carrinho();
                            carrinho.IdCarrinho = reader.GetInt32("IdCarrinho");
                            carrinho.IdProduto = reader.GetInt32("IdPruduto");
                            carrinho.IdUsuario = reader.GetInt32("IdUsuario");
                            carrinho.Quantidade = reader.GetInt32("Quantidade");
                            listacarrinho.Add(carrinho);
                        };
                    }
                }
                con.Close();
                return listacarrinho;
            }
        }

        public static List<ModCantina> listarCliente()
        {
            List<ModCantina> listacliente = new List<ModCantina>();
            string sql = "SELECT * FROM Usuarios";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModCantina cliente = new ModCantina();
                            cliente.IdUsuario = reader.GetInt32("IdUsuario");
                            cliente.Nome = reader.GetString("Nome");
                            cliente.Cpf = reader.GetString("Cpf");
                            cliente.Email = reader.GetString("Email");
                            cliente.Matricula = reader.GetInt32("Matricula");
                            cliente.Curso = reader.GetString("Curso");
                            cliente.Tipo = reader.GetString("Tipo");
                            cliente.Telefone = reader.GetString("Telefone");
                            listacliente.Add(cliente);
                        };
                    }
                }
                con.Close();
                return listacliente;
            }
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
                string query = "UPDATE Produtos SET Nome = @Nome, Descricao = @Descricao, Valor = @Valor, Tipo = @Tipo, Foto = @Foto WHERE IdProdutos = @IdProdutos";

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
                        cmd.Parameters.AddWithValue("@Id", produto.IdProdutos);
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
                string query = "DELETE FROM Produtos WHERE IdProdutos = @IdProdutos";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdProdutos", id);
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
                string query = "SELECT * FROM Produtos WHERE IdProdutos = @IdProdutos";

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdProdutos", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                produto.IdProdutos = Convert.ToInt32(reader["IdProdutos"]);
                                produto.Nome = reader["Nome"].ToString();
                                produto.Descricao = reader["Descricao"].ToString();
                                produto.Valor = Convert.ToDouble(reader["Valor"]);
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
                            produto.IdProdutos = reader.GetInt32("IdProdutos");
                            produto.Nome = reader.GetString("Nome");
                            produto.Descricao = reader.GetString("Descricao");
                            produto.Valor = reader.GetDouble("Valor");
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
    }
}
