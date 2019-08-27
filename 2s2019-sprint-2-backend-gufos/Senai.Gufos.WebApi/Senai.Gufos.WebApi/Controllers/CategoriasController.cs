using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Repositories;

namespace Senai.Gufos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        CategoriaRepository CategoriaRepository = new CategoriaRepository();

        /// <summary>
        /// Listar todas as categorias
        /// </summary>
        /// <returns>200 com a lista de categorias</returns>
        [Authorize]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(CategoriaRepository.Listar());
        }

        /// <summary>
        /// Cadastra uma nova categoria 
        /// </summary>
        /// <param name="categoria">Categorias</param>
        /// <returns>Mensagem de erro ou de sucesso</returns>
        [HttpPost]
        public IActionResult Cadastrar(Categorias categoria)
        {
            try
            {
                CategoriaRepository.Cadastrar(categoria);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(new { Mensagem = "Deu ruim cz:" + ex.Message });
            }

        }/////


        /// <summary>
        /// Busca uma categoria pelo seu id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Categorias</returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Categorias categoria = CategoriaRepository.BuscarPorId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }////


        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            CategoriaRepository.Deletar(id);
            return Ok();
        }


        [HttpPut]
        public IActionResult Atua1izar(Categorias categoria)
        {
            try
            {
                Categorias CategoriasBuscada = CategoriaRepository.BuscarPorId(categoria.IdCategoria);

                if (CategoriasBuscada == null)
                {
                    return NotFound();
                }
                CategoriaRepository.Atualizar(categoria);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(new { Mensagem = "Deu ruim cz:" + ex.Message });
            }
        }//

    }//------------------------------------------------------------------------------------------------------------------------------------------------
}