using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public string NumeroEmpleado { get; set; } = null!;

    public string? Rfc { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? FechaDeNacimiento { get; set; }

    public string? Nss { get; set; }

    public string? FechaIngreso { get; set; }

    public string? Foto { get; set; }

    public int? IdEmpresa { get; set; }

    public virtual ICollection<Dependiente> Dependientes { get; } = new List<Dependiente>();

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    //Propiedades Agregadas Empleado

    public string NombreEmpresa { get; set; }
}
