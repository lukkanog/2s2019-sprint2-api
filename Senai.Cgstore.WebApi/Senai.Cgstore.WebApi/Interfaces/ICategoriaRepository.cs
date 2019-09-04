using Senai.Cgstore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Cgstore.WebApi.Interfaces
{
    public interface ICategoriaRepository
    {
        /// <summary>
        ///Lista todas as categorias
        /// </summary>
        /// <returns>Lista de categorias</returns>
        List<Categorias> Listar();

        /// <summary>
        /// Cadastra uma nova categoria
        /// </summary>
        /// <param name="categoria">Categoria</param>
        void Cadastrar(Categorias categoria);

        /// <summary>
        /// Busca uma categoria por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Categoria</returns>
        Categorias BuscarPorId(int id);

        
    }
}
