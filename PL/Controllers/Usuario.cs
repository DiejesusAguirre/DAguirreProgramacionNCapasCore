using Microsoft.AspNetCore.Mvc;
using ML;
using NuGet.Protocol;
using System.Drawing;

namespace PL.Controllers
{
    public class Usuario : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public Usuario(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.ROL = new ML.Rol();
            ML.Result result = new ML.Result();
            //result = BL.Usuario.GetAll(usuario);
            ML.Result resultRol = BL.Rol.GetAll();

            try
            {
                //Se manda la url desde el appsettings
                string urlApi = _configuration["urlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);
                    //Aqui es como si hiciera el BL.Usuario.GetAll pero a los servicios
                    var responseTask = client.GetAsync("Usuario/GetAll");

                    responseTask.Wait();
                    var resultServices = responseTask.Result;

                    if (resultServices.IsSuccessStatusCode)
                    {
                        var readTask = resultServices.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Objects = new List<object>();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }


            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.ROL.Roles = resultRol.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Algo salioi mal";
                return View(usuario);
            }

        }
        [HttpPost]
        public ActionResult Index(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            //result = BL.Usuario.GetAll(usuario);
            usuario.ROL = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            ML.Result resultRol = BL.Rol.GetAll();

            try
            {
                //Se manda la url desde el appsettings
                string urlApi = _configuration["urlAPI"];
                using (var client = new HttpClient())
                {
                    usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
                    usuario.Sexo = (usuario.Sexo == null) ? "" : usuario.Sexo;
                    usuario.Email = (usuario.Email == null) ? "" : usuario.Email;
                    usuario.FechaDeNacimiento = (usuario.FechaDeNacimiento == null) ? "" : usuario.FechaDeNacimiento;
                    usuario.Password = (usuario.Password == null) ? "" : usuario.Password;
                    usuario.UserName = (usuario.UserName == null) ? "" : usuario.UserName;
                    usuario.Telefono = (usuario.Telefono == null) ? "" : usuario.Telefono;
                    usuario.Celular = (usuario.Celular == null) ? "" : usuario.Celular;
                    usuario.CURP = (usuario.CURP == null) ? "" : usuario.CURP;
                    usuario.Imagen = (usuario.Imagen == null) ? "" : usuario.Imagen;
                    usuario.NombreCompleto = (usuario.NombreCompleto == null) ? "" : usuario.NombreCompleto;
                    usuario.ConfirmPasword = (usuario.ConfirmPasword == null) ? "" : usuario.ConfirmPasword;
                    usuario.Direccion.Calle = (usuario.Direccion.Calle == null) ? "" : usuario.Direccion.Calle;
                    usuario.Direccion.NumeroExterior = (usuario.Direccion.NumeroExterior == null) ? "" : usuario.Direccion.NumeroExterior;
                    usuario.Direccion.NumeroInterior = (usuario.Direccion.NumeroInterior == null) ? "" : usuario.Direccion.NumeroInterior;
                    //usuario.Direccion.Direcciones = (usuario.Direccion.Direcciones == null) ?  : usuario.Direccion.Direcciones;
                    usuario.Direccion.Colonia.Nombre = (usuario.Direccion.Colonia.Nombre == null) ? "" : usuario.Direccion.Colonia.Nombre;
                    usuario.Direccion.Colonia.CodigoPostal = (usuario.Direccion.Colonia.CodigoPostal == null) ? "" : usuario.Direccion.Colonia.CodigoPostal;
                    //usuario.Direccion.Colonia.Colonias = (usuario.Direccion.Colonia.Colonias == null) ? 0 : usuario.Direccion.Colonia.Colonias;
                    usuario.Direccion.Colonia.Municipio.Nombre = (usuario.Direccion.Colonia.Municipio.Nombre == null) ? "" : usuario.Direccion.Colonia.Municipio.Nombre;
                    usuario.Direccion.Colonia.Municipio.Estado.Nombre = (usuario.Direccion.Colonia.Municipio.Estado.Nombre == null) ? "" : usuario.Direccion.Colonia.Municipio.Estado.Nombre;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = (usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre == null) ? "" : usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre;
                    usuario.ROL.IdRol = (usuario.ROL.IdRol == null) ? 0 : usuario.ROL.IdRol;
                    usuario.ROL.NombreROL = (usuario.ROL.NombreROL == null) ? "" : usuario.ROL.NombreROL;

                    client.BaseAddress = new Uri(urlApi);
                    //Aqui es como si hiciera el BL.Usuario.GetAll pero a los servicios
                    var responseTask = client.PostAsJsonAsync("Usuario/GetAll", usuario);

                    responseTask.Wait();
                    var resultServices = responseTask.Result;

                    if (resultServices.IsSuccessStatusCode)
                    {
                        var readTask = resultServices.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.ROL.Roles = resultRol.Objects;
            }
            else
            {
                ViewBag.Message = "Algo salioi mal";
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            usuario.ROL = new ML.Rol();
            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();

            if (IdUsuario == null)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.ROL.Roles = resultRol.Objects;
                return View(usuario);
            }
            else
            {
                //Update
                /*ML.Result result = BL.Usuario.GetById(IdUsuario.Value)*/
                ;
                try
                {
                    //Se manda la url desde el appsettings
                    string urlApi = _configuration["urlAPI"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlApi);
                        //Aqui es como si hiciera el BL.Usuario.GetAll pero a los servicios
                        var responseTask = client.GetAsync("Usuario/GetById/" + IdUsuario);

                        responseTask.Wait();
                        var resultServices = responseTask.Result;

                        if (resultServices.IsSuccessStatusCode)
                        {
                            var readTask = resultServices.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();


                            ML.Usuario resultItemList = new ML.Usuario();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;

                            result.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }

                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.ROL.Roles = resultRol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                    ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                    ML.Result resultDireccion = BL.Direccion.GetByIdColonia(usuario.Direccion.Colonia.IdColonia);
                    usuario.Direccion.Direcciones = resultDireccion.Objects;


                }
                else
                {
                    ViewBag.Message = "No existe";
                }

                return View(usuario);
            }

        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            IFormFile file = Request.Form.Files["IfImage"];
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }
            if (ModelState.IsValid)
            {
                if (usuario.IdUsuario == 0)
                {
                    try
                    {
                        //Se manda la url desde el appsettings
                        string urlApi = _configuration["urlAPI"];
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlApi);
                            //Aqui es como si hiciera el BL.Usuario.GetAll pero a los servicios
                            var responseTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);

                            responseTask.Wait();
                            var resultServices = responseTask.Result;

                            if (resultServices.IsSuccessStatusCode)
                            {
                                result.Correct = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }


                    //result = BL.Usuario.Add(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha registrado exitosamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Ha ocurrido un error" + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
                else
                {
                    //Update
                    //result = BL.Usuario.Update(usuario);
                    try
                    {
                        //Se manda la url desde el appsettings
                        string urlApi = _configuration["urlAPI"];
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlApi);
                            //Aqui es como si hiciera el BL.Usuario.GetAll pero a los servicios
                            var responseTask = client.PutAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);

                            responseTask.Wait();
                            var resultServices = responseTask.Result;

                            if (resultServices.IsSuccessStatusCode)
                            {
                                result.Correct = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }

                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha actualizado exitosamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Ha ocurrido un error";
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.ROL.Roles = resultRol.Objects;
                return View(usuario);
            }


        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            if (IdUsuario == null)
            {
                ViewBag.Message = "Algo ocurrio";
                return PartialView("Modal");
            }
            else
            {

                try
                {
                    //Se manda la url desde el appsettings
                    string urlApi = _configuration["urlAPI"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlApi);
                        //Aqui es como si hiciera el BL.Usuario.GetAll pero a los servicios
                        var responseTask = client.DeleteAsync("Usuario/Delete/" + IdUsuario);

                        responseTask.Wait();
                        var resultServices = responseTask.Result;

                        if (resultServices.IsSuccessStatusCode)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.ROL = new ML.Rol();
                            result = BL.Usuario.GetAll(usuario);

                            result.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                if (result.Correct)
                {
                    ViewBag.Message = "Se elimino correctamente";
                    return PartialView("Modal");
                }

                else
                {
                    ViewBag.Message = "Algo ocurrio" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }


        public JsonResult GetEstado(int IdPais)
        {


            var result = BL.Estado.GetByIdPais(IdPais);


            return Json(result.Objects);
        }

        public JsonResult GetMunicipio(int IdEstado)
        {


            var result = BL.Municipio.GetByIdEstado(IdEstado);


            return Json(result.Objects);
        }

        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }

        public static byte[] ConvertToBytes(IFormFile Imagen)
        {
            using var fileStream = Imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        public JsonResult CambiarStatus(int IdUsuario, bool Status)
        {


            var result = BL.Usuario.CambiarStatus(IdUsuario, Status);


            return Json(result);
        }
    }
}
