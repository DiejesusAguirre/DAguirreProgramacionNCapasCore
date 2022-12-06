﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Dependiente
    {
        public int IdDependiente { get; set; }
        public string NumeroEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaDeNacimiento { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string RFC { get; set; }
        public ML.DependienteTipo Tipo { get; set; }
        public ML.EstadoCivil Civil { get; set; }
        public ML.Empleado Empleado { get; set; }
        public List<object> Dependientes { get; set; }
    }
}
