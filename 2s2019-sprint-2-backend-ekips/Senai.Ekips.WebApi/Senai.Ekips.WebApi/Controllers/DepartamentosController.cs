using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.Repositories;

namespace Senai.Ekips.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        DepartamentoRepository departamentoRepository = new DepartamentoRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(departamentoRepository.Listar());
        }



        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var departamentoBuscado = departamentoRepository.BuscarPorId(id);
            if (departamentoBuscado == null)
            {
                return NotFound();
            }
            return Ok(departamentoBuscado);
        }


        [HttpPost]
        public IActionResult Cadastrar(Departamentos departamento)
        {
            try
            {
                departamentoRepository.Cadastrar(departamento);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Deu ruim: {ex.Message}" });
            }
        }


    }//#########################################################################################################
}