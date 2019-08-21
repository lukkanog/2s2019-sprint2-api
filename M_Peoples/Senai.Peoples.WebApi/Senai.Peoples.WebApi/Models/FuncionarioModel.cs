using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Models
{
    public class FuncionarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, insira o nome do funcionário")]
        public string Nome { get; set; }

        //[Required(ErrorMessage = "Por favor, insira o sobrenome do funcionário")]
        public string Sobrenome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string NomeCompleto { get; set; }
    }
}
