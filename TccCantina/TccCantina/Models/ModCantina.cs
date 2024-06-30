using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
using SkiaSharp;

namespace TccCantina.Models
{
    public class ModCantina
    {
        public int IdUsuario { get; set; }
        public String Nome { get; set; }
        public String Cpf { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public int Matricula { get; set; }
        public String Curso { get; set; }
        public String Tipo { get; set; }
        public byte[] Foto { get; set; }
        public String Telefone { get; set; }
    }

    public class Professores
    {
        public int IdProfessores { get; set; }
        public String Nome { get; set; }
        public int Matricula { get; set; }
    }

    public class Produtos
    {
        public int IdProdutos { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public Double Valor { get; set; }
        public String Tipo { get; set; }
        public byte[] Foto { get; set; }
    }

    public class CarrinhoFiltrado
    {
        public int IdCarrinhoF { get; set; }
        public String Nome { get; set; }
        public int Quantidade { get; set; }
        public Double Valor { get; set; }
    }

    public class TotalCarrinho
    {
        int IdTotalCarrinho { get; set; }
        public Double ValorTotal { get; set; }
    }
}
