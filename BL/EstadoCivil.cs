using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstadoCivil
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {
                    var codigo = context.EstadoCivils.FromSqlRaw($"EstadoCivilGetAll").ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.EstadoCivil estadoCivil = new ML.EstadoCivil();

                            estadoCivil.IdEstadoCivil = obj.IdEstadoCivil;
                            estadoCivil.Nombre = obj.Nombre;


                            result.Objects.Add(estadoCivil);

                        }
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
