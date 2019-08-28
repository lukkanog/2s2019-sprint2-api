using System;
using System.Collections.Generic;

namespace Senai.Ekips.WebApi.Domains
{
    public partial class Departamentos
    {
        public Departamentos()
        {
            Cargos = new HashSet<Cargos>();
        }

        public int IdDepartamento { get; set; }
        public string Nome { get; set; }

        public ICollection<Cargos> Cargos { get; set; }
    }
}
