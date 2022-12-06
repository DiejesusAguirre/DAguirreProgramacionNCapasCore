using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {
        public static ML.Result GetByIdColonia(int IdColonia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Direccions.FromSqlRaw($"DireccionGetByIdColonia {IdColonia}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                            ML.Direccion direccion = new ML.Direccion();

                            direccion.IdDireccion = codigo.IdDireccion;
                            direccion.Calle = codigo.Calle;
                            direccion.NumeroInterior = codigo.NumeroInterior;
                            direccion.NumeroExterior = codigo.NumeroExterior;

                            direccion.Colonia = new ML.Colonia();
                            direccion.Colonia.IdColonia = IdColonia;

                            result.Objects.Add(direccion);

                        
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
