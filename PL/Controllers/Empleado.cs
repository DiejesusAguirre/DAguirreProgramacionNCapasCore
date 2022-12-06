using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Empleado : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll();

            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.Message = "Algo salioi mal";
            }
            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(string? NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            ML.Result resultEmpleado = BL.Empresa.GetAll(empleado.Empresa);

            if (NumeroEmpleado == null)
            {
                empleado.Action = "Add";
                empleado.Empresa.Empresas = resultEmpleado.Objects;
                return View(empleado);
            }
            else
            {
                empleado.Action = "Update";
                //Update de Empleado -- Vista
                ML.Result result = BL.Empleado.GetById(NumeroEmpleado);

                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                }
                else
                {
                    ViewBag.Message = "No existe";
                }
            }
            empleado.Empresa.Empresas = resultEmpleado.Objects;
            return View(empleado);
        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            IFormFile file = Request.Form.Files["IfImage"];
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                empleado.Foto = Convert.ToBase64String(ImagenBytes);
            }
            if (empleado.Action == "Add")
            {
                //Agregar usuario -- EmpleadoAdd
                ML.Result result = new ML.Result();
                    result = BL.Empleado.Add(empleado);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha registrado correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Ha ocurido un problema" + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                //}
            }
            else
            {
                //Update del Empleado
                ML.Result result = BL.Empleado.Update(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ha ocurido un problema" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }

            return View(empleado);
        }
        [HttpGet]
        public ActionResult Delete(string? NumeroEmpleado)
        {
            ML.Result result = new ML.Result();

            if (NumeroEmpleado == null)
            {
                ViewBag.Message = "Algo ocurrio";
                return PartialView("Modal");
            }
            else
            {

                result = BL.Empleado.Delete(NumeroEmpleado);
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


        public static byte[] ConvertToBytes(IFormFile Foto)
        {
            using var fileStream = Foto.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
