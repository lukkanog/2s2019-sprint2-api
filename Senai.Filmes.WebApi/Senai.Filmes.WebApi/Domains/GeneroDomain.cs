using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Domains
{
    public class GeneroDomain
    {
        public int Id { get;set; }
        [Required(ErrorMessage = "Coloca o nome ai cz")] 
        public string Nome { get;set; }
    }
}
