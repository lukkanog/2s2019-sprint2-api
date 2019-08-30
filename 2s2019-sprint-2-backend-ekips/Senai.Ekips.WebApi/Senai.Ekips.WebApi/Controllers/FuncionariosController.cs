using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        FuncionarioRepository funcionarioRepository = new FuncionarioRepository();
        
        // ARRUMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA a permissao e tal
        [Authorize]
        [HttpGet]
        public  IActionResult Listar()
        {
            var usuario = HttpContext.User;

            if (usuario.HasClaim(ClaimTypes.Role,"ADMINISTRADOR"))
            {
                return Ok(funcionarioRepository.ListarTodos());

            }else if (usuario.HasClaim(ClaimTypes.Role,"COMUM"))
            {
                var idFuncionario = int.Parse(usuario.Claims.First(x=> x.Type == JwtRegisteredClaimNames.Jti).Value);
                return Ok(funcionarioRepository.BuscarPorId(idFuncionario));
            }
            else
            {
                return Forbid();
            }
            
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Cadastrar(Funcionarios funcionario)
        {
            try
            {
                funcionarioRepository.Cadastrar(funcionario);
                return Ok(new { Mensagem = "Deu bom"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Deu ruim, vê aí: {ex.Message}" });         
            }
        }



        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id,Funcionarios funcionario)
        {
            funcionario.IdFuncionario = id;
            try
            {
                funcionarioRepository.Atualizar(funcionario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Deu ruim, vê aí: {ex.Message}" });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                funcionarioRepository.Excluir(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Deu ruim, vê aí: {ex.Message}" });
            }
        }





    }
}