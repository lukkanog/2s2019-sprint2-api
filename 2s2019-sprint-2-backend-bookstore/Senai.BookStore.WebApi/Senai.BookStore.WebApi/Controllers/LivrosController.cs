using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.BookStore.WebApi.Domains;
using Senai.BookStore.WebApi.Repositories;

namespace Senai.BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        LivroRepository livroRepository = new LivroRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(livroRepository.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu esse erro aqui cz: {ex.Message}" });
            }
        }//endListar

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var livro = livroRepository.BuscarPorId(id);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }//endBuscar

        [HttpPost]
        public IActionResult Cadastrar(LivroDomain livro)
        {
            try
            {
                livroRepository.Cadastrar(livro);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu esse erro aqui cz: {ex.Message}" });
            }
        }//endCadastrar


        [HttpPut("{id}")]
        public IActionResult Alterar(int id,LivroDomain livro)
        {
            livro.IdLivro = id;
            try
            {
                livroRepository.Alterar(livro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu esse erro aqui cz: {ex.Message}" });
            }
        }//endAlterar


        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                livroRepository.Excluir(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu esse erro aqui cz: {ex.Message}" });
            }
        }//endExcluir

        [HttpGet("{idAutor}/livros")]
        public IActionResult ListarPorAutor(int idAutor)
        {
            try
            {
                return Ok(livroRepository.ListarPorAutor(idAutor));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu esse erro aqui cz: {ex.Message}" });
            }
        }//end


        [HttpGet("buscar/{genero}/livros")]
        public IActionResult ListarPorGenero(string genero)
        {
            try
            {
                return Ok(livroRepository.ListarPorGenero(genero));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu esse erro aqui cz: {ex.Message}" });
            }
        }
    }
}