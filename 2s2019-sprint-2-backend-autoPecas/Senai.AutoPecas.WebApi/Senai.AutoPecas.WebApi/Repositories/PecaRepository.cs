using Microsoft.EntityFrameworkCore;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.ViewModels;
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
                if (pecaBuscada == null)
                    return;

                pecaBuscada.Codigo = peca.Codigo;
                pecaBuscada.Descricao = peca.Descricao;
                pecaBuscada.Peso = peca.Peso;
                pecaBuscada.PesoCusto = peca.PesoCusto;
                pecaBuscada.PesoVenda = peca.PesoVenda;

                ctx.Pecas.Update(pecaBuscada);
                ctx.SaveChanges();
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

        public List<GanhoViewModel> CalcularGanho(int idFornecedor)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                List<GanhoViewModel> listaViewModel = new List<GanhoViewModel>();
                var lista = ctx.Pecas.Where(x => x.IdFornecedor == idFornecedor).ToList();

                foreach (var item in lista)
                {
                    GanhoViewModel ganho = new GanhoViewModel();
                    ganho.DescricaoProduto = item.Descricao;
                    ganho.ValorCusto = Convert.ToDouble(item.PesoCusto);
                    ganho.ValorVenda = Convert.ToDouble(item.PesoVenda);
                    var valorGanho = Convert.ToDouble(item.PesoVenda - item.PesoCusto);
                    ganho.ValorGanho = valorGanho;
                    ganho.PorcentagemGanho = ((double)item.PesoVenda / 100) * valorGanho;

                    listaViewModel.Add(ganho);

                }

                return listaViewModel;
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
