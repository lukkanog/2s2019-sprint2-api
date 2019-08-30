using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.AutoPecas.WebApi.Repositories;

namespace Senai.AutoPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class PecasController : ControllerBase
    {
        PecaRepository pecaRepository = new PecaRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            var usuario = HttpContext.User;

            var idUsuario = int.Parse(usuario.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

            return Ok(pecaRepository.Listar(idUsuario));
        }



    }//##################################################################################################################################
}