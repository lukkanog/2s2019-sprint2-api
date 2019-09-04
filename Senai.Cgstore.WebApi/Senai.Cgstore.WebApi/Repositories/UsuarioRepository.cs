using Microsoft.EntityFrameworkCore;
using Senai.Cgstore.WebApi.Domains;
using Senai.Cgstore.WebApi.Interfaces;
using Senai.Cgstore.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Cgstore.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (CgstoreContext ctx = new CgstoreContext())
            {
                Usuarios usuario = ctx.Usuarios.Include(x=> x.Permissao).FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                if (usuario == null)
                    return null;
                return usuario;
            }
        }
    }
}
