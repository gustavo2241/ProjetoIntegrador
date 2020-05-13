using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegradorWeb.Models
{
    public class EstoqueModel
    {
        public Int64 codProduto { get; set; }
        public string nomeProduto { get; set; }
        public string categoriaProduto { get; set; }
        public Int64 qtdProduto { get; set; }
        public string precoProduto { get; set; }
    }
}
