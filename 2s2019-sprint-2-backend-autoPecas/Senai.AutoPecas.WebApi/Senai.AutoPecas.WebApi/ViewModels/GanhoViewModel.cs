using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.ViewModels
{
    public class GanhoViewModel
    {
        public string DescricaoProduto { get; set; }
        public double ValorCusto { get; set; }
        public double ValorVenda { get; set; }
        public double ValorGanho { get; set; }
        public double PorcentagemGanho { get; set; }

    }//
}
