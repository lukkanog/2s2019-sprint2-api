using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Domains;
using Senai.Optus.WebApi.Repositories;

namespace Senai.Optus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ArtistasController : ControllerBase
    {
        ArtistaRepository artistaRepository = new ArtistaRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(artistaRepository.Listar());
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var artista = artistaRepository.BuscarPorId(id);
            if (artista == null)
                return NotFound(new { Mensagem = "Iiih vei nem tem esse artista ai" });

            return Ok(artista);
        }


        [HttpGet("buscarpornome/{nome}")]
        public IActionResult BuscarPorArtista(string nome)
        {
            var artista = artistaRepository.BuscarPorNome(nome);
            if (artista == null)
                return NotFound(new { Mensagem = "Iiih vei nem tem esse artista ai" });

            return Ok(artista);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Cadastrar(Artistas artista)
        {
            try
            {
                artistaRepository.Cadastrar(artista);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Iih moio - {ex.Message}" });
            }
        }


        



    }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}