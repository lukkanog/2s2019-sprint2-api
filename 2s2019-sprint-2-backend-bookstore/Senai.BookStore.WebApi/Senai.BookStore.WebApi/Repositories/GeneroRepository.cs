using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class GeneroRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_BookStore; User Id=sa;Pwd=132";


        /// <summary>
        /// Lista todos os gêneros cadastrados no banco de dados
        /// </summary>
        /// <returns>Uma lista de generos</returns>
        public List<GeneroDomain> Listar()
        {
            List<GeneroDomain> generos = new List<GeneroDomain>();
            string Query = "SELECT * FROM Generos";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        var genero = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                            Descricao = sdr["Descricao"].ToString()
                        };
                        generos.Add(genero);
                    }
                }
                return generos;
            }
        }//endListar

        /// <summary>
        /// Cadastra um novo gênero no Banco de Dados
        /// </summary>
        /// <param name="genero"></param>
        public void Cadastrar(GeneroDomain genero)
        {
            string Query = "INSERT INTO Generos (Descricao) VALUES (@Descricao)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query,con);
                cmd.Parameters.AddWithValue("@Descricao",genero.Descricao);
                cmd.ExecuteNonQuery();
            }

        }//endCadastrar




    }//END_CLASSE
}
