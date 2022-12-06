using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        [DisplayName("Nombre de la Empresa")]
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        [DisplayName("URL de Pagina web Oficial")]
        public string DireccionWeb { get; set; }
        public string Logo { get; set; }

        public List<Object> Empresas { get; set; }
    }
}
