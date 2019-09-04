using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Por favor, insira o email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor, insira a senha")]
        public string Senha { get; set; }
    }
}
