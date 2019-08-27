using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [StringLength(30,MinimumLength =5,ErrorMessage ="A senha tem q ter no mínimo 5 caracteres")]
        public string Senha { get; set; }

    }
}
