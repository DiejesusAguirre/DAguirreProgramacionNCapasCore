using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Municipios.FromSqlRaw($"MunicipioGetByIDEstado {IdEstado}").ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.Municipio Municipio = new ML.Municipio();

                            Municipio.IdMunicipio = obj.IdMunicipio;
                            Municipio.Nombre = obj.Nombre;

                            Municipio.Estado = new ML.Estado();
                            Municipio.Estado.IdEstado = IdEstado;

                            result.Objects.Add(Municipio);

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
