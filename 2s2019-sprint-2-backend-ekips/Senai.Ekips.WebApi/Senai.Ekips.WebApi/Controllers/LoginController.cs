﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Ekips.WebApi.Repositories;
using Senai.Ekips.WebApi.ViewModels;

namespace Senai.Ekips.WebApi.Controllers
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
            try
            {
                var usuario = usuarioRepository.TentarLogin(login);
                if (usuario == null)
                {
                    return NotFound(new { Mensagem = "Email ou senha inválidos" });
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Permissao.ToUpper())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ekips-chave-autenticacao"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                        issuer: "Ekips.WebApi",
                        audience: "Ekips.WebApi",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(1),
                        signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });


            }catch(Exception ex)
            {
                return BadRequest();
            }

        }//login



    }//##############################################################################################################################
}