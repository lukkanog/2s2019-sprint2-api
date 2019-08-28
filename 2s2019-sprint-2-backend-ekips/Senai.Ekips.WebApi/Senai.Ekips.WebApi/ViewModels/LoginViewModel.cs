using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [StringLength(30, MinimumLength = 5,ErrorMessage = "Por favor, insira pelo menos 6 caracteres na senha")]
        public string Senha { get; set; }
    }
}
