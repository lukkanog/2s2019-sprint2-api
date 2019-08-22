using Senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Repositories
{
    public class FilmeRepository
    {
        private string StringConexao =
            "Data Source=.\\SqlExpress; initial catalog=RoteiroFilmes; User Id=sa;Pwd=132";

        public List<FilmeDomain> Listar()
        {
            var filmes = new List<FilmeDomain>();

            string Query = "SELECT F.*,G.Nome AS Genero FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain();
                        filme.IdFilme = Convert.ToInt32(sdr["IdFilme"]);
                        filme.Titulo = sdr["Titulo"].ToString();
                        filme.Genero = new GeneroDomain
                        {
                            Id = Convert.ToInt32(sdr["IdGenero"]),
                            Nome = sdr["Genero"].ToString()
                        };
                        filmes.Add(filme);
                    }
                }
            }
            return filmes;
        }//metodo


        public FilmeDomain BuscarPorId(int id)
        {
            string Query = "SELECT F.*,G.Nome AS Genero FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero WHERE F.IdFilme = @Id";
            //string Query = "SELECT F.*,G.Nome AS Genero FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero WHERE F.IdFilme ="+id;

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query,con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            FilmeDomain filme = new FilmeDomain
                            {
                                IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                                Titulo = sdr["Titulo"].ToString(),
                                Genero = new GeneroDomain
                                {
                                    Id = Convert.ToInt32(sdr["IdGenero"]),  
                                    Nome = sdr["Genero"].ToString()
                                }
                            };
                            return filme;


                        }
                    }
                }
            }
            return null;
        }//,metodo

        public void Cadastrar(FilmeDomain filme)
        {
            string Query = "INSERT INTO Filmes (Titulo,IdGenero) VALUES (@Titulo,@IdGenero)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Titulo",filme.Titulo);
                cmd.Parameters.AddWithValue("@IdGenero", filme.GeneroId);

                cmd.ExecuteNonQuery();
            }
        }//cadastrar


        public List<FilmeDomain> BuscarPorGenero(int id)
        {
            List<FilmeDomain> filmes = new List<FilmeDomain>();
            string Query = "SELECT F.*,G.Nome AS Genero FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero WHERE F.IdGenero = @Id";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        var filme = new FilmeDomain();
                        filme.IdFilme = Convert.ToInt32(sdr["IdFilme"]);
                        filme.Titulo = sdr["Titulo"].ToString();
                        filme.Genero = new GeneroDomain
                        {
                            Id = Convert.ToInt32(sdr["IdGenero"]),
                            Nome = sdr["Genero"].ToString()
                        };
                        filmes.Add(filme);
                    }
                }
            }
            return filmes;
        }//buscarPorGenero

    }//classe
}//end of the world
