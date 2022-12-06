using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        //Campos de la Tabla
        public int IdUsuario { get; set; }
        //[Required]
        //[StringLength(50, MinimumLength = 5)]
        public string? Nombre { get; set; }

       
        [DisplayName("Apellido Paterno:")]
        public string? ApellidoPaterno { get; set; }

        [DisplayName("Apellido Materno:")]
        public string? ApellidoMaterno { get; set; }

        public string? NombreCompleto { get; set; }

        
        public string Sexo { get; set; }

        
        //[EmailAddress]
        public string? Email { get; set; }

        
        [DisplayName("Fecha de Nacimiento:")]
        public string FechaDeNacimiento { get; set; }

        //[Required]
        //[RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string? Password { get; set; }

        //[Required]
        public string? UserName { get; set; }

        //[Required]
        //[RegularExpression(@"^[0-9]+$")]
        public string? Telefono { get; set; }

        //[RegularExpression(@"^[0-9]+$")]
        public string? Celular { get; set; }

        public string? CURP { get; set; }

        public string? Imagen { get; set; }

        public List<Object>? Usuarios { get; set; }

        //Variables de Navegacion
        public ML.Rol? ROL { get; set; }
        public ML.Direccion? Direccion { get; set; }

        //Propiedades Extras
        [DisplayName("Confirmar Contraseña:")]
        public string? ConfirmPasword { get; set; }

        public bool Status { get; set; }

        



    }
}
