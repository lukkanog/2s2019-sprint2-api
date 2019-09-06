using System;
using System.Collections.Generic;

namespace Senai.ManualPecas.WebApi.Domains
{
    public partial class Pecas
    {
        public int IdPeca { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public List<Vendas> Vendas { get; set; }
    }
}
