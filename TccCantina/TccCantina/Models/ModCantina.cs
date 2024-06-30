using SQLite;
using System;

namespace TccCantina.Models
{
    [Table("Usuarios")]
    public class ModCantina
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Matricula { get; set; }
        public string Curso { get; set; }
        public string Tipo { get; set; }
        public byte[] Foto { get; set; }
        public string Telefone { get; set; }
    }

    [Table("Professores")]
    public class Professores
    {
        public int IdProfessores { get; set; }
        public string Nome { get; set; }
        public int Matricula { get; set; }
    }

    [Table("Produtos")]
    public class Produtos
    {
        public int IdProdutos { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public Double Valor { get; set; }
        public String Tipo { get; set; }
        public byte[] Foto { get; set; }
    }

    {
        public int IdCarrinho { get; set; }
        public int IdUsuario { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public Double Valor { get; set; }

    public class CarrinhoFiltrado
    {
        public int IdCarrinhoF { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public Double Valor { get; set; }
        public int IdCarrinho { get; set; }
    }

    [Table("ValorTotal")]
    public class TotalCarrinho
    {
        public int IdTotalCarrinho { get; set; }
        public double ValorTotal { get; set; }
    }
}
