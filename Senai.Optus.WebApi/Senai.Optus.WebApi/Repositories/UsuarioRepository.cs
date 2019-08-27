using Senai.Optus.WebApi.Domains;
using Senai.Optus.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Optus.WebApi.Repositories
{
    public class UsuarioRepository
    {
        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (OptusContext ctx = new OptusContext())
            {
                Usuarios usuarioBuscado = ctx.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);

                if (usuarioBuscado == null)
                    return null;

                return usuarioBuscado;
            }
        }
    }
}
