using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Cgstore.WebApi.Interfaces;
using Senai.Cgstore.WebApi.Repositories;
using Senai.Cgstore.WebApi.ViewModels;

namespace Senai.Cgstore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository UsuarioRepository { get; set; }

        public LoginController()
        {
            UsuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                var usuario = UsuarioRepository.BuscarPorEmailESenha(login);

                if (usuario == null)
                    return NotFound(new { Mensagem = "Email ou senha incorretos" });



                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, usuario.Permissao.Nome.ToString())
            };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("cgstore-chave-autenticacao"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                        issuer: "Cgstore.WebApi",
                        audience: "Cgstore.WebApi",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(1),
                        signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}