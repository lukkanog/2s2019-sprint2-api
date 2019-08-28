using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories
{
    public class UsuarioRepository
    {
        /// <summary>
        /// Busca um usuario atraves do email e senha passados como parametro
        /// </summary>
        /// <param name="login">LoginViewModel</param>
        /// <returns>Um usuario</returns>
        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (GufosContext ctx = new GufosContext())
            {
                Usuarios UsuarioBuscado = ctx.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);

                if (UsuarioBuscado == null)
                {
                    return null;
                }
                return UsuarioBuscado;
            }
        }//end_method

    }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
