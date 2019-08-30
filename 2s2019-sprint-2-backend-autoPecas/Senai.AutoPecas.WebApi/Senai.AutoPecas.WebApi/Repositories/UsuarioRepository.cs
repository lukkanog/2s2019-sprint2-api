using Senai.AutoPecas.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Repositories
{
    public class UsuarioRepository
    {

        public Usuarios TentarLogin(Usuarios usuario)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                //return ctx.Usuarios.FirstOrDefault(x => x.Email == usuario.Email && x.Senha == usuario.Senha);
                return ctx.Usuarios.Where(x => x.Email == usuario.Email && x.Senha == usuario.Senha).FirstOrDefault();
            }
        }

    }
}
