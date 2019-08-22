using Senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Repositories
{
    public class GeneroRepository
    {
        private string StringConexao =
            "Data Source=.\\SqlExpress; initial catalog=RoteiroFilmes; User Id=sa;Pwd=132";

        public List<GeneroDomain> Listar()
        {
            List<GeneroDomain> generos = new List<GeneroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT * FROM Generos ORDER BY IdGenero ASC";

                con.Open();
                SqlDataReader srd;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    srd = cmd.ExecuteReader();
                    
                    while (srd.Read())
                    {
                        GeneroDomain estilo = new GeneroDomain
                        {
                            Id = Convert.ToInt32(srd["IdGenero"]),
                            Nome = srd["Nome"].ToString()
                        };
                        generos.Add(estilo);
                    }
                }
            }
            return generos;
        }//                fim do metodo               // 


        public GeneroDomain BuscarPorId(int id)
        {
            string Query = "SELECT * FROM Generos WHERE IdGenero = @Id";

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
                            GeneroDomain genero = new GeneroDomain();
                            genero.Id = Convert.ToInt32(sdr["IdGenero"]);
                            genero.Nome = sdr["Nome"].ToString();
                            return genero;
                        }
                    }
                }
            }
            return null; 
        }//end



        public void Cadastrar(GeneroDomain genero)
        {
            string Query = "INSERT INTO Generos (Nome) VALUES (@Nome)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                cmd.ExecuteNonQuery();
            }
        }//                fim do metodo               //


        public void Alterar(GeneroDomain genero)
        {
            string Query = "UPDATE Generos SET Nome = @Nome WHERE IdGenero = @IdGenero";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@IdGenero", genero.Id);
                cmd.Parameters.AddWithValue("@Nome",genero.Nome);
                //cmd.Parameters.AddWithValue("@Nome", );
                cmd.ExecuteNonQuery();
            }
        }


        public void Deletar(int id)
        {
            string Query = "DELETE Generos WHERE IdGenero = @IdGenero";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@IdGenero",id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
