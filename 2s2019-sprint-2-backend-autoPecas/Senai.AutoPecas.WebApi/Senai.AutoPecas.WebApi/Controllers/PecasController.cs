using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.Repositories;

namespace Senai.AutoPecas.WebApi.Controllers
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
        public IActionResult Listar()
        {
            var usuario = HttpContext.User;

            var idUsuario = int.Parse(usuario.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

            return Ok(PecaRepository.Listar(idUsuario));
        }

        [HttpGet("{idPeca}")]
        public IActionResult BuscarPorId(int idPeca)
        {

            var usuario = HttpContext.User;
            int idUsuario = Convert.ToInt32(usuario.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

            var pecaBuscada = PecaRepository.BuscarPorId(idPeca,idUsuario);
            if (pecaBuscada == null)
                return NotFound(new { Mensagem = "Essa peça não existe ou não pertence a você" });

            return Ok(pecaBuscada);
        }

        [HttpPost]
        public IActionResult Cadastrar(Pecas peca)
        {

            try
            {

                var usuario = HttpContext.User;
                int idFornecedor = Convert.ToInt32(usuario.Claims.FirstOrDefault(x => x.Type == "FornecedorId").Value);
                peca.IdFornecedor = idFornecedor;

                PecaRepository.Cadastrar(peca);

                return Ok(new { Mensagem = "Peça cadastrada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ocorreu o seguinte erro: {ex.Message}"});
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Pecas peca,int id)
        {
            try
            {
                peca.IdPeca = id;

                var usuario = HttpContext.User;
                int idFornecedorLogado = Convert.ToInt32(usuario.Claims.FirstOrDefault(x => x.Type == "FornecedorId").Value);
                
                if (peca.IdFornecedor != idFornecedorLogado)
                {
                    return BadRequest(new { Mensagem = "Essa peça n pertence a você" });
                }

                PecaRepository.Atualizar(peca);
                return Ok(new { Mensagem = "Peça editada com sucesso" });
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
                var usuario = HttpContext.User;
                int idFornecedorLogado = Convert.ToInt32(usuario.Claims.FirstOrDefault(x => x.Type == "FornecedorId").Value);

                var peca = PecaRepository.BuscarPorId(id,idFornecedorLogado);
                if (peca == null)
                    return BadRequest();

                PecaRepository.Excluir(peca);
                return Ok(new { Mensagem = "Peça removida com sucesso" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Ocorreu o seguinte erro: {ex.Message}" });

            }
        }


    }//##################################################################################################################################
}