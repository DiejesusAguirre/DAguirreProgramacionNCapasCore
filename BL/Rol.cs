using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Rols.FromSqlRaw("GetAllRoles").ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.IdRol = obj.IdRol;
                            rol.NombreROL = obj.Rol1;

                            result.Objects.Add(rol);

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
