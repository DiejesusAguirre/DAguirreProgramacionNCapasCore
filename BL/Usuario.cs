using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Sexo}', '{usuario.Email}', '{usuario.Password}', '{usuario.UserName}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.ROL.IdRol}, '{usuario.FechaDeNacimiento}', '{usuario.Imagen}','{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");

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

        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");

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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"UsuarioUpdate '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Sexo}', '{usuario.Email}', '{usuario.Password}', '{usuario.UserName}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.ROL.IdRol}, '{usuario.FechaDeNacimiento}', '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}, {usuario.IdUsuario}");

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

        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {
                    usuario.ROL.IdRol = (usuario.ROL.IdRol == null) ? 0 : usuario.ROL.IdRol;
                    var codigo = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}', {usuario.ROL.IdRol}").ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.Usuario usuarioobj = new ML.Usuario();
                            usuarioobj.ROL = new ML.Rol(); 
                            usuarioobj.Direccion = new ML.Direccion();
                            usuarioobj.Direccion.Colonia = new ML.Colonia();
                            usuarioobj.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioobj.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuarioobj.IdUsuario = obj.IdUsuario;
                            usuarioobj.Nombre = obj.Nombre;
                            usuarioobj.ApellidoPaterno = obj.ApellidoPaterno;
                            usuarioobj.ApellidoMaterno = obj.ApellidoMaterno;
                            usuarioobj.Sexo = obj.Sexo;
                            usuarioobj.Email = obj.Email;
                            usuarioobj.Password = obj.Password;
                            usuarioobj.UserName = obj.UserName;
                            usuarioobj.Telefono = obj.Telefono;
                            usuarioobj.Celular = obj.Celular;
                            usuarioobj.CURP = obj.Curp;
                            usuarioobj.ROL.NombreROL = obj.Rol;
                            usuarioobj.FechaDeNacimiento = DateTime.Parse(obj.FechaDeNacimiento.ToString()).ToString("dd-MM-yyyy") + "\n \n \n";
                            usuarioobj.Imagen = obj.Imagen;
                            usuarioobj.Status = bool.Parse(obj.Status.ToString());
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;
                            usuarioobj.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                            usuarioobj.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuarioobj.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                            usuarioobj.Direccion.Calle = obj.Calle;
                            usuarioobj.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuarioobj.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuarioobj.NombreCompleto = usuarioobj.Nombre + " " + usuarioobj.ApellidoPaterno + " " + usuarioobj.ApellidoMaterno;
                            result.Objects.Add(usuarioobj);

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

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.ROL = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.IdUsuario = codigo.IdUsuario;
                        usuario.Nombre = codigo.Nombre;
                        usuario.ApellidoPaterno = codigo.ApellidoPaterno;
                        usuario.ApellidoMaterno = codigo.ApellidoMaterno;
                        usuario.Sexo = codigo.Sexo;
                        usuario.Email = codigo.Email;
                        usuario.Password = codigo.Password;
                        usuario.UserName = codigo.UserName;
                        usuario.Telefono = codigo.Telefono;
                        usuario.Celular = codigo.Celular;
                        usuario.CURP = codigo.Curp;
                        usuario.ROL.IdRol = (int) codigo.IdRol;
                        usuario.FechaDeNacimiento = DateTime.Parse(codigo.FechaDeNacimiento.ToString()).ToString("dd-MM-yyyy") + "\n \n \n";
                        usuario.Imagen = codigo.Imagen;
                        usuario.Status = (bool)codigo.Status;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = codigo.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = codigo.NombrePais;
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = codigo.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = codigo.NombreEstado;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = codigo.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = codigo.NombreMunicipio;
                        usuario.Direccion.Colonia.IdColonia = codigo.IdColonia;
                        usuario.Direccion.Colonia.Nombre = codigo.NombreColonia;
                        usuario.Direccion.IdDireccion = codigo.IdDireccion;
                        usuario.Direccion.Calle = codigo.Calle;
                        usuario.Direccion.NumeroExterior = codigo.NumeroExterior;
                        usuario.Direccion.NumeroInterior = codigo.NumeroInterior;

                        
                        result.Object = usuario;


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

        public static ML.Result ConvertExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter OleDBAdapter = new OleDbDataAdapter(cmd);
                        DataTable tableUsuario = new DataTable();

                        OleDBAdapter.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            //Esta sera la Lista
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableUsuario.Rows)
                            {

                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.Sexo = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.UserName = row[6].ToString();
                                usuario.Telefono = row[7].ToString();
                                usuario.Celular = row[8].ToString();
                                usuario.CURP = row[9].ToString();

                                usuario.ROL = new ML.Rol();
                                usuario.ROL.IdRol = int.Parse(row[10].ToString());

                                usuario.FechaDeNacimiento = row[11].ToString();

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[15].ToString());

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i = 1;

                foreach(ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    usuario.ROL = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();

                    usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingrese un Nombre" : usuario.Nombre;
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == "") ? error.Mensaje += "Ingrese un Apellido Paterno" : usuario.ApellidoPaterno;
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == "") ? error.Mensaje += "Ingrese un Apellido Materno" : usuario.ApellidoMaterno;
                    usuario.Sexo = (usuario.Sexo == "") ? error.Mensaje += "Ingrese un Sexo" : usuario.Sexo;
                    usuario.Email = (usuario.Email == "") ? error.Mensaje += "Ingrese un Email" : usuario.Email;
                    usuario.Password = (usuario.Password == "") ? error.Mensaje += "Ingrese una Password" : usuario.Password;
                    usuario.UserName = (usuario.UserName == "") ? error.Mensaje += "Ingrese un Nombre de Usuario" : usuario.UserName;
                    usuario.Telefono = (usuario.Telefono == "") ? error.Mensaje += "Ingrese un Telefono" : usuario.Telefono;
                    usuario.Celular = (usuario.Celular == "") ? error.Mensaje += "Ingrese un Numero Celular" : usuario.Celular;
                    usuario.CURP = (usuario.CURP == "") ? error.Mensaje += "Ingrese un CURP" : usuario.CURP;
                    if (usuario.ROL.IdRol.ToString() == "")
                    {
                        error.Mensaje += "Ingresa el ID de un Rol";
                    }
                    usuario.FechaDeNacimiento = (usuario.FechaDeNacimiento == "") ? error.Mensaje += "Ingrese una Fecha de Nacimiento" : usuario.FechaDeNacimiento;
                    usuario.Direccion.Calle = (usuario.Direccion.Calle == "") ? error.Mensaje += "Ingrese una Calle" : usuario.Direccion.Calle;
                    usuario.Direccion.NumeroInterior = (usuario.Direccion.NumeroInterior == "") ? error.Mensaje += "Ingrese un Numero Interior" : usuario.Direccion.NumeroExterior;
                    usuario.Direccion.NumeroExterior = (usuario.Direccion.NumeroExterior == "") ? error.Mensaje += "Ingrese un Numero Exterior" : usuario.Direccion.NumeroExterior;
                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        error.Mensaje += "Ingresa el ID de una Colonia";
                    }


                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }
                }
                result.Correct = true;
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result CambiarStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {

                    var codigo = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus {IdUsuario}, {Status}");

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

        public static ML.Result Login(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DaguirreProgramacionNcapasContext context = new DL.DaguirreProgramacionNcapasContext())
                {
                    var codigo = context.Usuarios.FromSqlRaw($"LoginGetByIdUserName '{usuario.UserName}'").AsEnumerable().FirstOrDefault();

                    if (codigo != null)
                    {

                        ML.Usuario usuarioobj = new ML.Usuario();
                        usuarioobj.ROL = new ML.Rol();

                        usuarioobj.Password = codigo.Password;
                        usuarioobj.UserName = codigo.UserName;
                        usuarioobj.ROL.IdRol = (int)codigo.IdRol;
                       

                        result.Object = usuarioobj;


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
