using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Filmes.WebApi.Domains;
using Senai.Filmes.WebApi.Repositories;

namespace Senai.Filmes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        FilmeRepository FilmeRepository = new FilmeRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(FilmeRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(new { Mensagem = $"Ocorreu o seguinte erro:{e.Message}" });
            }
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var filmeProcurado = FilmeRepository.BuscarPorId(id);

            if (filmeProcurado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(filmeProcurado);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(FilmeDomain filme)
        {
            try
            {
                FilmeRepository.Cadastrar(filme);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Mensagem = $"Deu ruim: {e.Message}"});
            }
        }


        [HttpGet("{id}/buscar")]
        public IActionResult BuscarPorGenero(int id)
        {
            try
            {
                return Ok(FilmeRepository.BuscarPorGenero(id));
            }catch(Exception e)
            {
                return BadRequest(new { Mensagem = $"Deu ruim: {e.Message}" });
            }
        }
        

    }
}