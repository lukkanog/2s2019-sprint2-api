using Microsoft.EntityFrameworkCore;
using Senai.Optus.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Optus.WebApi.Repositories
{
    public class EstiloRepository
    {
        /// <summary>
        /// Lista todos os estilos musicais
        /// </summary>
        /// <returns>Lista de estilos</returns>
        public List<Estilos> Listar()
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Estilos.ToList();
            }
        }//Listar()


        public void Cadastrar(Estilos estilo)
        {
            using (OptusContext ctx = new OptusContext())
            {
                ctx.Estilos.Add(estilo);
                ctx.SaveChanges();
            }
        }//Cadastrar()


        public Estilos BuscarPorId(int id)
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Estilos.FirstOrDefault(x => x.IdEstilo == id);
            }
        }//BuscarPorId()



        public void Deletar(int id)
        {
            using (OptusContext ctx = new OptusContext())
            {
                var estilo = ctx.Estilos.Find(id);
                ctx.Estilos.Remove(estilo);
                ctx.SaveChanges();
            }
        }//Deletar()

        public void Atualizar(Estilos estilo)
        {
            using (OptusContext ctx = new OptusContext())
            {
                var estiloBuscado = ctx.Estilos.FirstOrDefault(x => x.IdEstilo == estilo.IdEstilo);
                estiloBuscado.Nome = estilo.Nome;

                ctx.Estilos.Update(estiloBuscado);
                ctx.SaveChanges();
            }
        }


        public int QuantidadeEstilos()
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Estilos.Count();
            }
        }

        public List<Artistas> ListarArtistasPorEstilo(int idEstilo)
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Artistas.Where(x => x.IdEstilo == idEstilo).ToList();
            }
        }


        //public List<Artistas> ListarArtistasPorNomeEstilo(string estilo)
        //{
        //    using (OptusContext ctx = new OptusContext())
        //    {
        //        return ctx.Artistas.Include(x.IdAr)
        //    }
        //}

    }//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~a~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
