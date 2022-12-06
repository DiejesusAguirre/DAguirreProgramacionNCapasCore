using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Empleados.FromSqlRaw($"EmpleadoGetAll").ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.Empleado empleadoobj = new ML.Empleado();
                            empleadoobj.Empresa = new ML.Empresa();

                            empleadoobj.NumeroEmpleado = obj.NumeroEmpleado;
                            empleadoobj.RFC = obj.Rfc;
                            empleadoobj.Nombre = obj.Nombre;
                            empleadoobj.ApellidoPaterno = obj.ApellidoPaterno;
                            empleadoobj.ApellidoMaterno = obj.ApellidoMaterno;
                            empleadoobj.Email = obj.Email;
                            empleadoobj.Telefono = obj.Telefono;
                            empleadoobj.FechaNacimiento = DateTime.Parse(obj.FechaDeNacimiento.ToString()).ToString("dd-MM-yyyy") + "\n \n \n";
                            empleadoobj.NSS = obj.Nss;
                            empleadoobj.FechaIngreso = DateTime.Parse(obj.FechaIngreso.ToString()).ToString("dd-MM-yyyy") + "\n \n \n";
                            empleadoobj.Foto = obj.Foto;
                            empleadoobj.Empresa.IdEmpresa = int.Parse(obj.IdEmpresa.ToString());
                            empleadoobj.Empresa.Nombre = obj.NombreEmpresa;

                            result.Objects.Add(empleadoobj);

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

        public static ML.Result GetById(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Empleados.FromSqlRaw($"EmpleadoGetById '{NumeroEmpleado}'").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {

                        ML.Empleado empleado = new ML.Empleado();
                        //Instancia de la variable de navegacion
                        empleado.Empresa = new ML.Empresa();

                        empleado.NumeroEmpleado = codigo.NumeroEmpleado;
                        empleado.RFC = codigo.Rfc;
                        empleado.Nombre = codigo.Nombre;
                        empleado.ApellidoPaterno = codigo.ApellidoPaterno;
                        empleado.ApellidoMaterno = codigo.ApellidoMaterno;
                        empleado.Email = codigo.Email;
                        empleado.Telefono = codigo.Telefono;
                        empleado.FechaNacimiento = DateTime.Parse(codigo.FechaDeNacimiento.ToString()).ToString("dd-MM-yyyy") + "\n \n \n";
                        empleado.NSS = codigo.Nss;
                        empleado.FechaIngreso = DateTime.Parse(codigo.FechaIngreso.ToString()).ToString("dd-MM-yyyy") + "\n \n \n";
                        empleado.Foto = codigo.Foto;
                        empleado.Empresa.IdEmpresa = int.Parse(codigo.IdEmpresa.ToString());
                        empleado.Empresa.Nombre = codigo.NombreEmpresa;

                        result.Object = empleado;


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

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");

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

        public static ML.Result Delete(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{NumeroEmpleado}'");

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

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");

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

        //public static bool ExisteConteoGetById(string NumeroEmpleado)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
        //        {

        //            var codigo = context.Empleados.FromSqlRaw($"SELECT COUNT(*) FROM Empleado WHERE NumeroEmpleado='{NumeroEmpleado}'").Count();
                    

        //            if (codigo.Count > 0)
        //            {
        //                result.Correct = false;
        //            }
        //            else
        //            {
        //                result.Correct = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //    }

        //    return result.Correct;
        //}

    }
}
