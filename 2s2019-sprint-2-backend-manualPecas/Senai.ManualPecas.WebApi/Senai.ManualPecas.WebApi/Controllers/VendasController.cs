using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Interfaces;
using Senai.ManualPecas.WebApi.Repositories;
using Senai.ManualPecas.WebApi.ViewModels;

namespace Senai.ManualPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private IVendaRepository VendaRepository { get; set; }

        public VendasController()
        {
            VendaRepository = new VendaRepository();
        }

        [HttpPost("{idPeca}")]
        public IActionResult Cadastrar(VendaViewModel vendaVm, int idPeca)
        {
            try
            {
                var usuario = HttpContext.User;

                int idFornecedor = Convert.ToInt32(usuario.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value);



                var venda = new Vendas();
                venda.IdPeca = idPeca;
                venda.IdFornecedor = idFornecedor;
                venda.Preco = vendaVm.Preco;

                VendaRepository.Cadastrar(venda);
                return Ok();
            
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####//#####
}