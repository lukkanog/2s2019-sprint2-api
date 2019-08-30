using Microsoft.EntityFrameworkCore;
using Senai.AutoPecas.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Repositories
{
    public class PecaRepository
    {

        public List<Pecas> Listar(int id)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                return ctx.Pecas.Include(p => p.IdFornecedorNavigation).Where(p => p.IdFornecedor == id).ToList();
            }
        }

    }
}
