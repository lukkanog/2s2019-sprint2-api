using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Models;
using Senai.Peoples.WebApi.Repositories;
using Senai.Peoples.WebApi.Viewmodels;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        FuncionarioRepository funcionarioRepository = new FuncionarioRepository();

        [HttpGet]
        public IEnumerable<FuncionarioModel> Listar()
        {
            return funcionarioRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var funcionarioProcurado = funcionarioRepository.BuscarPorId(id);

            if (funcionarioProcurado == null)
            {
                return NotFound();
            }
            return Ok(funcionarioProcurado);
        }

        [HttpPost]
        public IActionResult Cadastrar(FuncionarioModel funcionario)
        {
            funcionarioRepository.Cadastrar(funcionario);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id,FuncionarioModel funcionario)
        {
            funcionario.Id = id;
            funcionarioRepository.Alterar(funcionario);
            return Ok();
        }//METODO

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            funcionarioRepository.Excluir(id);
            return Ok();
        }//METODO

        [HttpGet("nomescompletos")]
        public IEnumerable<NomeCompletoViewmodel> ListarNomesCompletos()
        {
            return funcionarioRepository.ListarNomesCompletos();
        }

        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            var funcionario = funcionarioRepository.BuscarPorNome(nome);
            if (funcionario == null)
            {
                return NotFound();
            }
            return Ok(funcionario);
        }

        [HttpGet("ordenacao/{ordem}")]
        public IActionResult Ordenar(string ordem)
        {
            if (!ordem.Equals("asc") && !ordem.Equals("desc"))
            {
                return BadRequest();
            }
             var lista = funcionarioRepository.Ordenar(ordem);
            return Ok(lista);
        }
    }
}