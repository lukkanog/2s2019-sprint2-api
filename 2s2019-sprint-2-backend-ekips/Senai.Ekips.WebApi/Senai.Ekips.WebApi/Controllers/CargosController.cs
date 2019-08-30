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
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class CargosController : ControllerBase
    {
        CargoRepository cargoRepository = new CargoRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(cargoRepository.Listar());
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cargo = cargoRepository.BuscarPorId(id);
            if (cargo == null)
                return NotFound();
            return Ok(cargo);
        }

        [HttpPost]
        public IActionResult Cadastrar(Cargos cargo)
        {
            try
            {
                cargoRepository.Cadastrar(cargo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = "Deu ruim:" + ex.Message });
            }
        }


        [HttpPut("{id}")]
        public IActionResult Atualizar(int id,Cargos cargo)
        {
            cargo.IdCargo = id;
            try
            {
                cargoRepository.Atualizar(cargo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = "Deu ruim:" + ex.Message });
            }
        }

    }//#############################################################################################################################
}