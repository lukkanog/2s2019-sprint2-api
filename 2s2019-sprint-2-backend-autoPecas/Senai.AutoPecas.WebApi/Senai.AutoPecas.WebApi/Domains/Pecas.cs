using System;
using System.Collections.Generic;

namespace Senai.AutoPecas.WebApi.Domains
{
    public partial class Pecas
    {
        public int IdPeca { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal? Peso { get; set; }
        public decimal PesoCusto { get; set; }
        public decimal PesoVenda { get; set; }
        public int? IdFornecedor { get; set; }

        public Fornecedores IdFornecedorNavigation { get; set; }
    }
}
