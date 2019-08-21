    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Filmes.WebApi.Domains;
using Senai.Filmes.WebApi.Repositories;

namespace Senai.Filmes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        GeneroRepository generoRepository = new GeneroRepository();

        [HttpGet]
        public IEnumerable<GeneroDomain> Listar()
        {
            return generoRepository.Listar();
            
        }

        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain genero)
        {
            generoRepository.Cadastrar(genero);
            return Ok();
        }

        //[HttpPut("{id}")]
        //public IActionResult Alterar(int id)
        //{
        //    generoRepository.Alterar(id);
        //    return Ok();
        //}


        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            generoRepository.Deletar(id);
            return Ok();
        }
    }
}