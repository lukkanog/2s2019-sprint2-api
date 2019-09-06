using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        public void Cadastrar(Vendas venda)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                ctx.Vendas.Add(venda);
                ctx.SaveChanges();
            }
        }
    }
}
