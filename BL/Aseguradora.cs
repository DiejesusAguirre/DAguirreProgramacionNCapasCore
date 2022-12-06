using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}");

                    if (codigo > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int IdAseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"AseguradoraDelete {IdAseguradora}");

                    if (codigo > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradora.IdAseguradora}, '{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}");

                    if (codigo > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {
                    var codigo = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = (DateTime)obj.FechaCreacion;
                            aseguradora.FechaModificacion = (DateTime)obj.FechaModificacion;
                            aseguradora.Usuario.Nombre = obj.NombreUsuario;
                            aseguradora.Usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            aseguradora.Usuario.ApellidoMaterno = obj.ApellidoMaterno;


                            result.Objects.Add(aseguradora);

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

        public static ML.Result GetById(int IdAseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById {IdAseguradora}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {

                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.IdAseguradora = codigo.IdAseguradora;
                        aseguradora.Nombre = codigo.Nombre;
                        aseguradora.FechaCreacion = (DateTime)codigo.FechaCreacion;
                        aseguradora.FechaModificacion = (DateTime)codigo.FechaModificacion;
                        aseguradora.Usuario.IdUsuario = codigo.IdUsuario;
                        aseguradora.Usuario.Nombre = codigo.NombreUsuario;
                        aseguradora.Usuario.ApellidoPaterno = codigo.ApellidoPaterno;
                        aseguradora.Usuario.ApellidoMaterno = codigo.ApellidoMaterno;


                        result.Object = aseguradora;


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
