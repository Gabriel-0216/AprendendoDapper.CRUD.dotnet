using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Dapper.dotnet.Entities
{
    public class Produto
    {
        [Key]
        [Display(Name ="Id")]
        public int ProdutoId { get; set; }

        [Display(Name ="Nome do produto")]
        [Required(ErrorMessage ="É necessário informar o nome do produto")]
        [StringLength(25, ErrorMessage ="O nome deve ter entre 1 até 100 caracteres")]
        public string NomeProduto { get; set; }

        [Required]
        [Display(Name ="Estoque")]
        [Range(1, Int32.MaxValue, ErrorMessage ="Valor deve ser maior que zero.")]
        public int Estoque { get; set; }

        [Required]
        [Display(Name ="Preço")]
        public decimal Preco { get; set; }
    }
}
