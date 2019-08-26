using Senai.Gufos.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories
{
    public class CategoriaRepository
    {
        /// <summary>
        /// Listar todas as categorias
        /// </summary>
        /// <returns>Lista de Categorias</returns>
        public List<Categorias> Listar()
        {
            using (GufosContext ctx = new GufosContext())
            {
                ///SELECT * FROM Categorias
                return ctx.Categorias.ToList();
            }
        }//end_Listar()


        /// <summary>
        /// Cadastra uma nova Categoria
        /// </summary>
        /// <param name="categoria">Categorias</param>
        public void Cadastrar(Categorias categoria)
        {
            using (GufosContext ctx = new GufosContext())
            {
                //INSERT INTO Categorias
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
            }
        }//end_method


        /// <summary>
        ///Busca uma categoria pelo seu id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Categoria</returns>
        public Categorias BuscarPorId(int id)
        {
            using (GufosContext ctx = new GufosContext())
            {
                //SELECT WHERE
                return ctx.Categorias.FirstOrDefault(x => x.IdCategoria == id);
            }
        }//////


        public void Deletar(int id)
        {
            using (GufosContext ctx = new GufosContext())
            {
                //DEWLETE FROM Categorias WHERE IdCategoria = id
                Categorias categoriaBuscada = ctx.Categorias.Find(id);
                ctx.Categorias.Remove(categoriaBuscada);
                ctx.SaveChanges();
            }
        }///////--

        public void Atualizar(Categorias categoria)
        {
            using (GufosContext ctx = new GufosContext())
            {
                Categorias CategoriaBuscada = ctx.Categorias.FirstOrDefault(x => x.IdCategoria == categoria.IdCategoria);
                CategoriaBuscada.Nome = categoria.Nome;

                ctx.Categorias.Update(CategoriaBuscada);
                ctx.SaveChanges();
            }
        }


    }//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
