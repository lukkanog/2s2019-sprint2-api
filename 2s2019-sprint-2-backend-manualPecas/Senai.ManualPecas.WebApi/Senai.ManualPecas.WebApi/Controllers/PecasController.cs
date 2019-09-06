using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Interfaces;
using Senai.ManualPecas.WebApi.Repositories;

namespace Senai.ManualPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class PecasController : ControllerBase
    {
        private IPecaRepository PecaRepository { get; set; }

        public PecasController()
        {
            PecaRepository = new PecaRepository();
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(PecaRepository.ListarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var pecaBuscada = PecaRepository.BuscarPorId(id);
            if (pecaBuscada == null)
                return NotFound();

            return Ok(pecaBuscada);

        }

        [HttpPost]
        public IActionResult Cadastrar(Pecas peca)
        {
            try
            {
                PecaRepository.Cadastrar(peca);
                return Ok(new { Mensagem = "Peça cadastrada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ocorreu o seguinte erro: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Pecas peca, int id)
        {
            try
            {
                peca.IdPeca = id;
                PecaRepository.Atualizar(peca);
                return Ok(new { Mensagem = "Peça editada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ocorreu o seguinte erro: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                PecaRepository.Excluir(id);
                return Ok(new { Mensagem = "Peça removida com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ocorreu o seguinte erro: {ex.Message}" });
            }
        }


    }//############################################################################################################################
}