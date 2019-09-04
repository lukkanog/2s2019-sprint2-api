using Microsoft.EntityFrameworkCore;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Repositories
{
    public class PecaRepository : IPecaRepository
    {

   
        public void Atualizar(Pecas peca)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                var pecaBuscada = ctx.Pecas.Find(peca.IdPeca);
                pecaBuscada = peca;

                ctx.Pecas.Update(pecaBuscada);
            }
        }

        public Pecas BuscarPorId(int idPeca,int idUsuario)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                Pecas peca = ctx.Pecas.Find(idPeca);
                if (peca == null || peca.IdFornecedor != idUsuario)
                    return null;

                return peca;
            }
        }

        public void Cadastrar(Pecas peca)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {

                ctx.Pecas.Add(peca);
                ctx.SaveChanges();

            }
        }


        public void Excluir(Pecas peca)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                ctx.Pecas.Remove(peca);
                ctx.SaveChanges();
            }
        }




        public List<Pecas> Listar(int idFornecedor)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                return ctx.Pecas.Where(x => x.IdFornecedor == idFornecedor).ToList();
            }
        }
    }
}
