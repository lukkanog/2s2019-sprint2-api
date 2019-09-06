using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Interfaces;
using Senai.ManualPecas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        public Fornecedores BuscarPorEmailESenha(LoginViewModel login)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                var usuario = ctx.Fornecedores.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                if (usuario == null)
                    return null;

                return usuario;
            }
        }

        public void Cadastrar(Fornecedores fornecedor)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                ctx.Fornecedores.Add(fornecedor);
                ctx.SaveChanges();
            }
        }
    }//####################################################################################################################################
}
