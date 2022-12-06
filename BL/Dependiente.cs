using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {
                    var codigo = context.Dependientes.FromSqlRaw($"DependienteGetAll").ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.Tipo = new ML.DependienteTipo();
                            dependiente.Civil = new ML.EstadoCivil();

                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.NumeroEmpleado = obj.NumeroEmpleado;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaDeNacimiento = obj.FechaDeNacimiento;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.Tipo.IdDependienteTipo = (int)obj.IdDependientoTipo;
                            dependiente.Tipo.Nombre = obj.TipoDependiente;
                            dependiente.Civil.IdEstadoCivil = (int)obj.IdEstadoCivil;
                            dependiente.Civil.Nombre = obj.EstadoCivil;

                            
                            
                            result.Objects.Add(dependiente);

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

        public static ML.Result GetById(int IdDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Dependientes.FromSqlRaw($"DependienteGetById {IdDependiente}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {

                        ML.Dependiente dependiente = new ML.Dependiente();
                        dependiente.Tipo = new ML.DependienteTipo();

                        dependiente.IdDependiente = codigo.IdDependiente;
                        dependiente.NumeroEmpleado = codigo.NumeroEmpleado;
                        dependiente.Nombre = codigo.Nombre;
                        dependiente.ApellidoPaterno = codigo.ApellidoPaterno;
                        dependiente.ApellidoMaterno = codigo.ApellidoMaterno;
                        dependiente.FechaDeNacimiento = codigo.FechaDeNacimiento;
                        dependiente.Genero = codigo.Genero;
                        dependiente.Telefono = codigo.Telefono;
                        dependiente.RFC = codigo.Rfc;
                        dependiente.Tipo.IdDependienteTipo = (int)codigo.IdDependientoTipo;
                        dependiente.Tipo.Nombre = codigo.TipoDependiente;
                        dependiente.Civil.IdEstadoCivil = (int)codigo.IdEstadoCivil;
                        dependiente.Civil.Nombre = codigo.EstadoCivil;


                        result.Object = dependiente;


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

        public static ML.Result Add(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"DependienteAdd '{dependiente.NumeroEmpleado}','{dependiente.Nombre}','{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}','{dependiente.FechaDeNacimiento}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.RFC}',{dependiente.Tipo.IdDependienteTipo}, {dependiente.Civil.IdEstadoCivil}");

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

        public static ML.Result Delete(int IdDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"DependienteDelete {IdDependiente}");

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

        public static ML.Result Update(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"DependienteUpdate '{dependiente.NumeroEmpleado}','{dependiente.Nombre}','{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}','{dependiente.FechaDeNacimiento}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.RFC}',{dependiente.Tipo.IdDependienteTipo},{dependiente.IdDependiente}, {dependiente.Civil.IdEstadoCivil}");

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

        //EmpleadoGetByIdDependiente

        public static ML.Result GetById(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Dependientes.FromSqlRaw($"EmpleadoGetByIdDependiente '{NumeroEmpleado}'").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {

                        ML.Dependiente dependiente = new ML.Dependiente();
                        dependiente.Tipo = new ML.DependienteTipo();
                        dependiente.Civil = new ML.EstadoCivil();

                        dependiente.IdDependiente = codigo.IdDependiente;
                        dependiente.NumeroEmpleado = codigo.NumeroEmpleado;
                        dependiente.Nombre = codigo.Nombre;
                        dependiente.ApellidoPaterno = codigo.ApellidoPaterno;
                        dependiente.ApellidoMaterno = codigo.ApellidoMaterno;
                        dependiente.FechaDeNacimiento = codigo.FechaDeNacimiento;
                        dependiente.Genero = codigo.Genero;
                        dependiente.Telefono = codigo.Telefono;
                        dependiente.RFC = codigo.Rfc;
                        dependiente.Tipo.IdDependienteTipo = (int)codigo.IdDependientoTipo;
                        dependiente.Tipo.Nombre = codigo.TipoDependiente;
                        dependiente.Civil.IdEstadoCivil = (int)codigo.IdEstadoCivil;
                        dependiente.Civil.Nombre = codigo.EstadoCivil;


                        result.Object = dependiente;


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
