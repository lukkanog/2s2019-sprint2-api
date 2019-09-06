using Microsoft.EntityFrameworkCore;
using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Repositories
{
    public class PecaRepository : IPecaRepository
    {
        public void Atualizar(Pecas peca)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                var pecaBuscada = ctx.Pecas.Find(peca.IdPeca);
                if (pecaBuscada == null)
                    return;

                pecaBuscada.Codigo = peca.Codigo;
                pecaBuscada.Descricao = peca.Descricao;

                ctx.Pecas.Update(pecaBuscada);
                ctx.SaveChanges();
            }
        }

        public Pecas BuscarPorId(int id)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                var peca = ctx.Pecas.Include(x => x.Vendas).FirstOrDefault(x => x.IdPeca == id);
                if (peca == null)
                    return null;

                return peca;
            }
        }

        public void Cadastrar(Pecas peca)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                ctx.Pecas.Add(peca);
                ctx.SaveChanges();
            }
        }

        public void Excluir(int id)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                var peca = ctx.Pecas.Find(id);
                ctx.Pecas.Remove(peca);
                ctx.SaveChanges();
            }
        }

        public List<Pecas> ListarTodos()
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                return ctx.Pecas.Include(x => x.Vendas).ToList();
            }
        }
    }//########//########//########//########//########//########//########//########//########//########//########//########//########
}
