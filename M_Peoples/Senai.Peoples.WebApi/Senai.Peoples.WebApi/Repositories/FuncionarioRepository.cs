using Senai.Peoples.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_Peoples; User Id=sa;Pwd=132";

        public List<FuncionarioModel> Listar()
        {
            List<FuncionarioModel> listaFuncionarios = new List<FuncionarioModel>();
            string Query = "SELECT * FROM Funcionarios ORDER BY IdFuncionario DESC";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while(sdr.Read())
                    {
                        FuncionarioModel funcionario = new FuncionarioModel();
                        funcionario.Id = Convert.ToInt32(sdr["IdFuncionario"]);
                        funcionario.Nome = sdr["Nome"].ToString();
                        funcionario.Sobrenome = sdr["Sobrenome"].ToString();
                        funcionario.DataNascimento = Convert.ToDateTime(sdr["DataNascimento"]);
                        funcionario.NomeCompleto = $"{funcionario.Nome} {funcionario.Sobrenome}";
                        listaFuncionarios.Add(funcionario);
                    }
                }
            }

            return listaFuncionarios;
        }//metodo


        public void Cadastrar(FuncionarioModel funcionario)
        {
            string Query = "INSERT INTO Funcionarios (Nome,Sobrenome) VALUES (@Nome,@Sobrenome)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome",funcionario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);

                cmd.ExecuteNonQuery();
               
            }
        }//metodo


        public FuncionarioModel BuscarPorId(int id)
        {
            string Query = "SELECT * FROM Funcionarios WHERE IdFuncionario = @Id";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            FuncionarioModel funcionario = new FuncionarioModel();
                            funcionario.Id = Convert.ToInt32(sdr["IdFuncionario"]);
                            funcionario.Nome = sdr["Nome"].ToString();
                            funcionario.Sobrenome = sdr["Sobrenome"].ToString();
                            funcionario.DataNascimento = Convert.ToDateTime(sdr["DataNascimento"]);
                            funcionario.NomeCompleto = $"{funcionario.Nome} {funcionario.Sobrenome}";
                            return funcionario;
                        }
                    }
                }
            }
            return null;
        }//metodo


        public void Alterar(FuncionarioModel funcionario)
        {
            string Query = "UPDATE Funcionarios SET Nome = @Nome WHERE IdFuncionario = @Id  UPDATE Funcionarios SET Sobrenome = @Sobrenome WHERE IdFuncionario = @Id UPDATE Funcionarios SET DataNascimento = @Data WHERE IdFuncionario = @Id";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome",funcionario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome",funcionario.Sobrenome);
                cmd.Parameters.AddWithValue("@Data",funcionario.DataNascimento.ToShortDateString());
                cmd.Parameters.AddWithValue("@Id", funcionario.Id);
                cmd.ExecuteNonQuery();
            }
        }


        public void Excluir(int id)
        {
            string Query = "DELETE Funcionarios WHERE IdFuncionario = @IdFuncionario";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@IdFuncionario", id);
                cmd.ExecuteNonQuery();
            }
        }//metodo

        public List<FuncionarioModel> ListarNomesCompletos()
        {
            var listaVelha = Listar();
            var listaNova = new List<FuncionarioModel>();
            foreach (var item in listaVelha)
            {
                if (item != null)
                {
                    FuncionarioModel funcionario = new FuncionarioModel();
                    funcionario.NomeCompleto = $"{item.Nome} {item.Sobrenome}";
                    listaNova.Add(funcionario);
                }
            }
            return listaNova;
        }//METODO


        public FuncionarioModel BuscarPorNome(string nome)
        {
            string Query = "SELECT * FROM Funcionarios WHERE Nome LIKE @Nome";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FuncionarioModel funcionario = new FuncionarioModel();
                        funcionario.Id = Convert.ToInt32(sdr["IdFuncionario"]);
                        funcionario.Nome = sdr["Nome"].ToString();
                        funcionario.Sobrenome = sdr["Sobrenome"].ToString();
                        funcionario.DataNascimento = Convert.ToDateTime(sdr["DataNascimento"]);
                        funcionario.NomeCompleto = $"{funcionario.Nome} {funcionario.Sobrenome}";
                        return funcionario;
                    }
                }
            }
            return null;
        }

        public List<FuncionarioModel> Ordenar(string ordem)
        {
            var listaOrdenada = new List<FuncionarioModel>();
            string Query = "SELECT * FROM Funcionarios ORDER BY Nome " + ordem;

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    //cmd.Parameters.AddWithValue("@Ordem", ordem);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        FuncionarioModel funcionario = new FuncionarioModel();
                        funcionario.Id = Convert.ToInt32(sdr["IdFuncionario"]);
                        funcionario.Nome = sdr["Nome"].ToString();
                        funcionario.Sobrenome = sdr["Sobrenome"].ToString();
                        funcionario.DataNascimento = Convert.ToDateTime(sdr["DataNascimento"]);
                        funcionario.NomeCompleto = $"{funcionario.Nome} {funcionario.Sobrenome}";
                        listaOrdenada.Add(funcionario);
                    }
                }
            }
            return listaOrdenada;
        }
    }
}
