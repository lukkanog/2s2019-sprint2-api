using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Domains
{
    public class Vendas
    {
        public int IdPeca { get; set; }
        public Pecas Peca { get; set; }

        public int IdFornecedor { get; set; }
        public Fornecedores Fornecedor { get; set; }

        public decimal Preco { get; set; }
    }
}
