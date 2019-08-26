using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Domains;
using Senai.Optus.WebApi.Repositories;

namespace Senai.Optus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EstilosController : ControllerBase
    {
        EstiloRepository estiloRepository = new EstiloRepository();
            
        /// <summary>
        /// Lista todos os estilos
        /// </summary>
        /// <returns>Lista de todos os estilos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(estiloRepository.Listar());
        }//Listar()

        [HttpPost]
        public IActionResult Cadastrar(Estilos estilo)
        {
            try
            {
                estiloRepository.Cadastrar(estilo);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Iih moio - {ex.Message}" });
            }
        }//Cadastrar()

        [HttpGet("{id}")]
        public IActionResult BuscarPorid(int id)
        {
            var estiloBuscado = estiloRepository.BuscarPorId(id);

            if (estiloBuscado == null)
            {
                return NotFound();
            }

            return Ok(estiloBuscado);
        }//BuscarPorId

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            estiloRepository.Deletar(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar(Estilos estilo)
        {
            try
            {
                var estiloBuscado = estiloRepository.BuscarPorId(estilo.IdEstilo);

                if (estiloBuscado == null)
                {
                    return NotFound();
                }
                estiloRepository.Atualizar(estilo);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Iih moio - {ex.Message}" });
            }
        }

    }//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}