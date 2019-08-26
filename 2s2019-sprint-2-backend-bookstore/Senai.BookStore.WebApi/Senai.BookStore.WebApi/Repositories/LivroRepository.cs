using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class LivroRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_BookStore; User Id=sa;Pwd=132";

        /// <summary>
        /// Lista todos os livros cadastrados
        /// </summary>
        /// <returns>Uma lista de livros</returns>
        public List<LivroDomain> Listar()
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT * FROM vwLivros";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        var livro = new LivroDomain
                        {
                            IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Descricao = sdr["Descricao"].ToString(),

                            Autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Autor"].ToString(),
                                Email = sdr["Email"].ToString(),
                                DataNascimento = DateTime.Parse(sdr["DataNascimento"].ToString())
                            },

                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Genero"].ToString()
                            }

                        };
                        livros.Add(livro);
                    }
                }
            }
            return livros;
        }//endListar

        /// <summary>
        /// Busca determinado livro  no banco de dados pelo seu id, passado pelo controller
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LivroDomain</returns>
        public LivroDomain BuscarPorId(int id)
        {
            string Query = "SELECT * FROM vwLivros WHERE IdLivro = @Id";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Id",id);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            var livro = new LivroDomain();

                            livro.IdLivro = Convert.ToInt32(sdr["IdLivro"]);
                            livro.Titulo = sdr["Titulo"].ToString();
                            livro.Descricao = sdr["Descricao"].ToString();

                            livro.Autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Autor"].ToString(),
                                Email = sdr["Email"].ToString(),
                                DataNascimento = DateTime.Parse(sdr["DataNascimento"].ToString())
                            };
                            livro.IdAutor = livro.Autor.IdAutor;


                            livro.Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Genero"].ToString()
                            };
                            livro.IdGenero = livro.Genero.IdGenero;
                            
                            return livro;

                        }
                    }
                }
            }
            return null;
        }//endBuscar

        /// <summary>
        /// Cadastra um novo livro no banco de dados
        /// </summary>
        /// <param name="livro" type="LivroDomain"></param>
        public void Cadastrar(LivroDomain livro)
        {
            string Query = "INSERT INTO Livros (Titulo,Descricao,IdAutor,IdGenero) VALUES (@Titulo,@Descricao,@IdAutor,@IdGenero)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@Descricao", livro.Descricao);
                cmd.Parameters.AddWithValue("@IdAutor", livro.IdAutor);
                cmd.Parameters.AddWithValue("@IdGenero", livro.IdGenero);

                cmd.ExecuteNonQuery();

            }
        }//endCadastrar

        /// <summary>
        /// Altera determinado livro de acordo com o Json passado pelo Controller
        /// </summary>
        /// <param name="livro"  type="LivroDomain"></param>
        public void Alterar(LivroDomain livro)
        {
            string Query = "EXEC AlterarLivro @Titulo, @Descricao, @Id ";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Titulo",livro.Titulo);
                cmd.Parameters.AddWithValue("@Descricao", livro.Descricao);
                cmd.Parameters.AddWithValue("@Id", livro.IdLivro);

                cmd.ExecuteNonQuery();
            }
        }//endALterar

        /// <summary>
        /// Busca no banco de dados o registro que tem o id e o exclui
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            string Query = "DELETE Livros WHERE IdLivro = @Id";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Id",id);
                cmd.ExecuteNonQuery();
            }
        }//endExcluir

        /// <summary>
        /// Lista as obras do autor selecionado pelo id do mesmo
        /// </summary>
        /// <param name="idAutor"></param>
        /// <returns>Lista de livros do determinado autor</returns>
        public List<LivroDomain> ListarPorAutor(int idAutor)
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT * FROM vwLivros WHERE IdAutor = @Id";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Id",idAutor);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        var livro = new LivroDomain();

                        livro.IdLivro = Convert.ToInt32(sdr["IdLivro"]);
                        livro.Titulo = sdr["Titulo"].ToString();
                        livro.Descricao = sdr["Descricao"].ToString();

                        livro.Autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            Nome = sdr["Autor"].ToString(),
                            Email = sdr["Email"].ToString(),
                            DataNascimento = DateTime.Parse(sdr["DataNascimento"].ToString())
                        };
                        livro.IdAutor = livro.Autor.IdAutor;


                        livro.Genero = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                            Descricao = sdr["Genero"].ToString()
                        };
                        livro.IdGenero = livro.Genero.IdGenero;

                        livros.Add(livro);
                    }
                }
            }
            return livros;
        }//end
 
        public List<LivroDomain> ListarPorGenero(string genero)
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT * FROM vwLivros WHERE Genero LIKE @Genero";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Genero", genero);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        var livro = new LivroDomain();

                        livro.IdLivro = Convert.ToInt32(sdr["IdLivro"]);
                        livro.Titulo = sdr["Titulo"].ToString();
                        livro.Descricao = sdr["Descricao"].ToString();

                        livro.Autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            Nome = sdr["Autor"].ToString(),
                            Email = sdr["Email"].ToString(),
                            DataNascimento = DateTime.Parse(sdr["DataNascimento"].ToString())
                        };
                        livro.IdAutor = livro.Autor.IdAutor;


                        livro.Genero = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                            Descricao = sdr["Genero"].ToString()
                        };
                        livro.IdGenero = livro.Genero.IdGenero;

                        livros.Add(livro);
                    }
                }
            }
            return livros;
        }///////

    }
}
