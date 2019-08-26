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
    public class AutoresController : ControllerBase
    {
        AutorRepository autorRepository = new AutorRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(autorRepository.Listar());
            }catch(Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu um erro: {ex.Message}" });
            }
        }//endListar

        [HttpPost]
        public IActionResult Cadastrar(AutorDomain autor)
        {
            try
            {
                autorRepository.Cadastrar(autor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu um erro: {ex.Message}" });
            }
        }//endCadastrar

        
        [HttpGet("ativos")]
        public IActionResult ListarAtivos()
        {
            try
            {
                return Ok(autorRepository.ListarAtivos());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ish deu um erro: {ex.Message}" });
            }
        }
    }
}