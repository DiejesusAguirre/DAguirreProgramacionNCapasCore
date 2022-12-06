using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Aseguradora
    {
        public int IdAseguradora { get; set; }
        [Required]
        [DisplayName("Nombre de la Aseguradora")]
        public string Nombre { get; set; }
        public ML.Usuario Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public List<Object> Aseguradoras { get; set; }

    }
}
