using System;
using System.Collections.Generic;

namespace DL;

public partial class DependienteTipo
{
    public int IdDependientoTipo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Dependiente> Dependientes { get; } = new List<Dependiente>();
}
