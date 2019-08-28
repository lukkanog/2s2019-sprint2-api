using Senai.Ekips.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories
{
    public class FuncionarioRepository
    {
        ////se for usar o SqlClient:
        private string StringConexao = $"Data Source=.\\SqlExpress; initial catalog=M_Ekips; User Id=sa;Pwd=132";



        public List<Funcionarios> ListarTodos()
        {
            List<Funcionarios> funcionarios = new List<Funcionarios>();
            string Query = "SELECT * FROM vwFuncionarios ORDER BY IdFuncionario DESC";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Funcionarios funcionario = new Funcionarios();
                        funcionario.IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"]);
                        funcionario.IdCargo = Convert.ToInt32(sdr["IdCargo"]);
                        funcionario.IdUsuario = Convert.ToInt32(sdr["IdUsuario"]);
                        funcionario.Nome = sdr["Nome"].ToString();
                        funcionario.DataNascimento = DateTime.Parse(sdr["DataNascimento"].ToString());
                        funcionario.Salario = Convert.ToDecimal(sdr["Salario"]);
                        funcionario.Cpf = sdr["Cpf"].ToString();

                        funcionario.IdCargoNavigation = new Cargos
                        {
                            IdCargo = Convert.ToInt32(funcionario.IdCargo),
                            Nome = sdr["Cargo"].ToString(),
                            EstaAtivo = Convert.ToBoolean(sdr["EstaAtivo"]),
                            IdDepartamentoNavigation = new Departamentos
                            {
                                IdDepartamento = Convert.ToInt32(sdr["IdFuncionario"]),
                                Nome = sdr["Departamento"].ToString()
                            }
                        };

                        funcionario.IdUsuarioNavigation = new Usuarios
                        {
                            IdUsuario = Convert.ToInt32(funcionario.IdUsuario),
                            Email = sdr["Email"].ToString(),
                            Senha = sdr["Senha"].ToString(),
                            Permissao = sdr["Permissao"].ToString()
                        };

                        funcionarios.Add(funcionario);
                    }//while
                }//cmd
            }//con

            return funcionarios;
        }


        public void Cadastrar(Funcionarios funcionario)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                ctx.Funcionarios.Add(funcionario);
                ctx.SaveChanges();
            }
        }


        public Funcionarios BuscarPorId(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                var funcionario = ctx.Funcionarios.Find(id);
                if (funcionario == null)
                    return null;

                return funcionario;
            }
        }



        public void Atualizar(Funcionarios funcionario)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                var funcionarioBuscado = ctx.Funcionarios.FirstOrDefault(x => x.IdFuncionario == funcionario.IdFuncionario);
                funcionarioBuscado.Nome = funcionario.Nome;
                funcionarioBuscado.Cpf = funcionario.Cpf;
                funcionarioBuscado.Salario = funcionario.Salario;
                funcionarioBuscado.DataNascimento = funcionario.DataNascimento;
                funcionarioBuscado.IdCargo = funcionario.IdCargo;
                funcionarioBuscado.IdUsuario = funcionario.IdUsuario;

                ctx.Funcionarios.Update(funcionarioBuscado);
                ctx.SaveChanges();
            }
        }

        public void Excluir(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                var funcionarioBuscado = ctx.Funcionarios.Find(id);
                ctx.Funcionarios.Remove(funcionarioBuscado);
                ctx.SaveChanges();
            }
        }



    }//########################################################################################################################
}
