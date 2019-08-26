using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class AutorRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_BookStore; User Id=sa;Pwd=132";

        /// <summary>
        /// Lista todos os autores cadastrados no banco de dados
        /// </summary>
        /// <returns>Uma listra de autores e suas características</returns>
        public List<AutorDomain> Listar()
        {
            List<AutorDomain> autores = new List<AutorDomain>();
            string Query = "SELECT * FROM Autores";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        var autor = new AutorDomain();
                        autor.IdAutor = Convert.ToInt32(sdr["IdAutor"]);
                        autor.Nome = sdr["Nome"].ToString();
                        autor.Email = sdr["Email"].ToString();
                        int ativo = Convert.ToInt32(sdr["Ativo"]);
                        //autor.EstaAtivo = sdr.GetBoolean(ativo);
                        switch (ativo)
                        {
                            case 1:
                                autor.EstaAtivo = true;
                            break;

                            case 0:
                                autor.EstaAtivo = false;
                            break;

                            default:
                            break;

                        }
                        autor.DataNascimento = DateTime.Parse(sdr["DataNascimento"].ToString());

                        autores.Add(autor);
                    }
                }
            }
            return autores;
        }//endListar

        /// <summary>
        /// Cadastra um novo autor no banco de dados
        /// </summary>
        /// <param name="autor"></param>
        public void Cadastrar(AutorDomain autor)
        {
            string Query = "INSERT INTO Autores (Nome,Email,Ativo,DataNascimento) VALUES (@Nome,@Email,@Ativo,@DataNascimento)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome",autor.Nome);
                cmd.Parameters.AddWithValue("@Email", autor.Email);

                int ativo = 1;
                switch (autor.EstaAtivo)
                {
                    case true:
                        ativo = 1;
                        break;
                    case false:
                        ativo = 0;
                        break;
                    default:
                        ativo = 1;
                        break;
                }
                cmd.Parameters.AddWithValue("@Ativo", ativo);
                cmd.Parameters.AddWithValue("@DataNascimento", autor.DataNascimento);

                cmd.ExecuteNonQuery();

            }
        }//end

        public List<AutorDomain> ListarAtivos()
        {
            List<AutorDomain> autoresAtivos = new List<AutorDomain>();
            foreach (var autor in Listar())
            {
                if (autor != null && autor.EstaAtivo == true)
                {
                    autoresAtivos.Add(autor);
                }
            }
            return autoresAtivos;
        }//end


    }//END CLASSE ----------
}
