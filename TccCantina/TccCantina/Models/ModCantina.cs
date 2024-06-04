using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;

namespace TccCantina.Models
{
    public class ModCantina
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Cpf { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public int Matricula { get; set; }
        public String Curso { get; set; }
        public String Tipo { get; set; }
        public byte[] Foto { get; set; }
    }

    public class Professores
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public int Matricula { get; set; }
    }

    public class Produtos
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public Decimal Valor { get; set; }
        public String Tipo { get; set; }
        public byte[] Foto { get; set; }
    }

    public class Carrinho 
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public byte[] Foto { get; set; }
    }

    public class CarrinhoFiltrado 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

    public class TotalCarrinho
    {
        public decimal ValorTotal { get; set; }
    }
}
