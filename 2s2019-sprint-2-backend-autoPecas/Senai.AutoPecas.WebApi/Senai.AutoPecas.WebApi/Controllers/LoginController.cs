using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.Repositories;
using Senai.AutoPecas.WebApi.ViewModels;

namespace Senai.AutoPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //UsuarioRepository usuarioRepository = new UsuarioRepository();

        private IUsuarioRepository UsuarioRepository {get;set;}

        public LoginController()
        {
            UsuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                var usuarioLogado = UsuarioRepository.BuscarPorEmailESenha(login);
                if (usuarioLogado == null)
                {
                    return NotFound( new { Mensagem = "Email ou senha inválidos, meu chapa"});
                }

                var fornecedor = usuarioLogado.Fornecedores.FirstOrDefault(x => x.IdUsuario == usuarioLogado.IdUsuario);
                int idFornecedor = fornecedor.IdFornecedor;

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioLogado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioLogado.IdUsuario.ToString()),
                    new Claim("FornecedorId",idFornecedor.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("autopecas-chave-autenticacao"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                        issuer: "AutoPecas.WebApi",
                        audience: "AutoPecas.WebApi",
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: creds);



                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token)});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = "Deu ruim olha ai:" + ex.Message });
            }
        }


    }
}