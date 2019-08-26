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
    [Produces("application/json")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        GeneroRepository generoRepository = new GeneroRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(generoRepository.Listar());
            } catch(Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu um erro: {ex.Message}"});
            }
        }//endListar

        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain genero)
        {
            try
            {
                generoRepository.Cadastrar(genero);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu um erro: {ex.Message}" });
            }
        }//endCadastrar
    }
}