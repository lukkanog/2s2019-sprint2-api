using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories
{
    public class UsuarioRepository
    {
        ////se for usar o SqlClient:
        //private string StringConexao = $"Data Source=.\\SqlExpress; initial catalog=M_Ekips; User Id=sa;Pwd=132";

        public Usuarios TentarLogin(LoginViewModel login)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                var usuarioBuscado = ctx.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);

                if (usuarioBuscado == null)
                    return null;

                return usuarioBuscado;
            }
        }//








    }//###################################################################################################################################
}
