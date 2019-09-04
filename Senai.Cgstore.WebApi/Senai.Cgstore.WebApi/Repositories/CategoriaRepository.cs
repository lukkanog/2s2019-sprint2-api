using Senai.Cgstore.WebApi.Domains;
using Senai.Cgstore.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Cgstore.WebApi.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public Categorias BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Categorias categoria)
        {
            using (CgstoreContext ctx = new CgstoreContext())
            {
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
            }
        }

        public List<Categorias> Listar()
        {
            using (CgstoreContext ctx = new CgstoreContext())
            {
                return ctx.Categorias.ToList();
            }
        }
    }
}
