using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAll(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Empresas.FromSqlRaw($"EmpresaGetAll '{empresa.Nombre}'").ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.Empresa empresaobj = new ML.Empresa();

                            empresaobj.IdEmpresa = obj.IdEmpresa;
                            empresaobj.Nombre = obj.Nombre;
                            empresaobj.Telefono = obj.Telefono;
                            empresaobj.Email = obj.Email;
                            empresaobj.DireccionWeb = obj.DireccionWeb;
                            empresaobj.Logo = obj.Logo;

                            result.Objects.Add(empresaobj);

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

        public static ML.Result GetById(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {

                        ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = codigo.IdEmpresa;
                        empresa.Nombre = codigo.Nombre;
                        empresa.Telefono = codigo.Telefono;
                        empresa.Email = codigo.Email;
                        empresa.DireccionWeb = codigo.DireccionWeb;
                        empresa.Logo = codigo.Logo;


                        result.Object = empresa;


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

        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}','{empresa.Telefono}','{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");

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

        public static ML.Result Delete(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"EmpresaDelete {IdEmpresa}");

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

        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"EmpresaUpdate {empresa.IdEmpresa},'{empresa.Nombre}','{empresa.Telefono}','{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");

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

    }
}
