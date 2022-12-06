using Microsoft.AspNetCore.Mvc;
using ML;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PL.Controllers
{
    public class CargaMasiva : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasiva(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult CargaExcel()
        {
            ML.Result result = new Result();
            return View(result);
        }
        [HttpPost]
        public ActionResult CargaTXT()
        {
            IFormFile fileTXT = Request.Form.Files["archivoTXT"];
            string fileError = @"C:\Users\digis\Documents\Vega Aguirre Diego Jesus\ArchivosTexto\ErroresLayoutl.txt";
            StreamWriter streamWriter = new StreamWriter(fileError, true, Encoding.ASCII);
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();

            if (fileTXT != null)
            {
                StreamReader Textfile = new StreamReader(fileTXT.OpenReadStream());

                int lineasCorrectas = 0, lineasTotal = 0;
                string line;
                line = Textfile.ReadLine();
                while ((line = Textfile.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');

                    usuario.Nombre = lines[0];
                    usuario.ApellidoPaterno = lines[1];
                    usuario.ApellidoMaterno = lines[2];
                    usuario.Sexo = lines[3];
                    usuario.Email = lines[4];
                    usuario.Password = lines[5];
                    usuario.UserName = lines[6];
                    usuario.Telefono = lines[7];
                    usuario.Celular = lines[8];
                    usuario.CURP = lines[9];

                    usuario.ROL = new ML.Rol();
                    usuario.ROL.IdRol = int.Parse(lines[10]);
                    usuario.FechaDeNacimiento = lines[11];
                    usuario.Imagen = null;

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = lines[12];
                    usuario.Direccion.NumeroInterior = lines[13];
                    usuario.Direccion.NumeroExterior = lines[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

                    usuario.NombreCompleto = usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno;
                    result = BL.Usuario.Add(usuario);


                    if (result.Correct)
                    {
                        streamWriter.WriteLine("Se registro Correctamente" + usuario.NombreCompleto);
                        lineasCorrectas++;
                    }
                    else
                    {
                        streamWriter.WriteLine("El Usuario " + usuario.NombreCompleto + " No se Registro Correctamente por el siguiente Error" + result.ErrorMessage);
                    }
                    lineasTotal++;
                }
                if (lineasCorrectas == lineasTotal)
                {
                    ViewBag.Message = "Carga De Usuarios Exitosa";
                    streamWriter.WriteLine(ViewBag.Message);
                    streamWriter.Close();
                }
                else
                {
                    ViewBag.Message = "Hubo Errores En La Carga, Favor De Verificar el Archivo";
                    streamWriter.Close();
                }

            }
            else
            {

                ViewBag.Message = "El Archivo No Se Encontro";
                streamWriter.WriteLine(ViewBag.Message);

                streamWriter.Close();

            }
            return PartialView("Modal");
        }
        [HttpPost]
        public ActionResult CargaExcel(ML.Usuario usuario)
        {

            IFormFile archivo = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (archivo.Length > 0)
                {
                    //que sea .xlsx
                    string fileName = Path.GetFileName(archivo.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        //crear copia
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                archivo.CopyTo(stream);
                            }
                            //leer la cadena de coneccion
                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            //se manda a llamar el metodo para convertirlo en un DataTable
                            ML.Result resultConvert = BL.Usuario.ConvertExceltoDataTable(connectionString);
                            //Validacion si la lista esta correcta
                            if (resultConvert.Correct)
                            {
                                ML.Result resultValidation = BL.Usuario.ValidarExcel(resultConvert.Objects);
                                if (resultValidation.Objects.Count == 0)
                                {
                                    resultValidation.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }
                                return View(resultValidation);
                            }
                            else
                            {
                                ViewBag.Message = "El archivo no se pudo leer correctamente";
                                return View("Modal");
                            }
                        }
                    }

                }
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertExceltoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioL in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.Add(usuarioL);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Usuario con el nombre: " + usuarioL.Nombre + " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        //string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"~\Files\logErrores.txt");
                        string fileError = _hostingEnvironment.WebRootPath + @"\Files\logErrores.txt";
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Usuarios No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los Usuarios han sido registrados correctamente";
                        return PartialView("Modal");
                    }
                }
            }
            return PartialView("Modal");
        }
    }
}
