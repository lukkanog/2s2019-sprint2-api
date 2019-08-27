using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Optus.WebApi.Repositories;
using Senai.Optus.WebApi.ViewModels;

namespace Senai.Optus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UsuarioRepository usuarioRepository = new UsuarioRepository();


        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            var usuario = usuarioRepository.BuscarPorEmailESenha(login);

            if (usuario == null)
            {
                return NotFound(new { mensagem = "Email ou senha inválidos." });
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario.Permissao),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("optus-chave-autenticacao"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: "Optus.WebApi",
                    audience: "Optus.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }//loginnnnnnnnnnnn

    }
}