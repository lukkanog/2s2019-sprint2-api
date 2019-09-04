using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Cgstore.WebApi.Domains;
using Senai.Cgstore.WebApi.Interfaces;
using Senai.Cgstore.WebApi.Repositories;

namespace Senai.Cgstore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private ICategoriaRepository CategoriaRepository { get; set; }

        public CategoriasController()
        {
            CategoriaRepository = new CategoriaRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(CategoriaRepository.Listar());
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Cadastrar(Categorias categoria)
        {
            try
            {
                //pegar o id do token
                int UsuarioId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

                categoria.UsuarioId = UsuarioId;
                categoria.DataCriacao = DateTime.Now;
                CategoriaRepository.Cadastrar(categoria);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = "deu ruim" + ex.Message });
            }
        }
    }
}