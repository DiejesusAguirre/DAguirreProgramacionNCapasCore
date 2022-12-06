using System;
using System.Collections.Generic;

namespace DL;

public partial class Dependiente
{
    public int IdDependiente { get; set; }

    public string? NumeroEmpleado { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? FechaDeNacimiento { get; set; }

    public string? Genero { get; set; }

    public string? Telefono { get; set; }

    public string? Rfc { get; set; }

    public int? IdDependientoTipo { get; set; }

    public int? IdEstadoCivil { get; set; }

    public virtual DependienteTipo? IdDependientoTipoNavigation { get; set; }

    public virtual EstadoCivil? IdEstadoCivilNavigation { get; set; }

    public virtual Empleado? NumeroEmpleadoNavigation { get; set; }

    //Propiedades Agregadas en Dependiente
    public string TipoDependiente { get; set; }
    public string EstadoCivil { get; set; }
    //public string IDEmpleado { get; set; }
    //public string NombreEmpleado { get; set; }
    //public string ApellidoPaternoEmpleado { get; set; }
    //public string ApellidoMaternoEmpleado { get; set; }
    //public string NSS { get; set; }
    //public string Foto { get; set; }
    //public string RFCEmpleado { get; set; }
    //public string FechaEmpleado { get; set; }
}
