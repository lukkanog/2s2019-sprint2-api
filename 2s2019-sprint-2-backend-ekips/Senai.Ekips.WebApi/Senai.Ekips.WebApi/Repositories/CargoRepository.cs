using Senai.Ekips.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories
{
    public class CargoRepository
    {
        public List<Cargos> Listar()
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.ToList();
            }
        }


        public Cargos BuscarPorId(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.Find(id);
            }
        }


        public void Cadastrar(Cargos cargo)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                ctx.Cargos.Add(cargo);
                ctx.SaveChanges();
            }
        }

        
        public void Atualizar(Cargos cargo)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                var cargoBuscado = ctx.Cargos.FirstOrDefault(x => x.IdCargo == cargo.IdCargo);
                cargoBuscado.Nome = cargo.Nome;
                cargoBuscado.EstaAtivo = cargo.EstaAtivo;
                cargoBuscado.IdDepartamento = cargo.IdDepartamento;

                ctx.Cargos.Update(cargoBuscado);
                ctx.SaveChanges();
            }
        }

    }
}
