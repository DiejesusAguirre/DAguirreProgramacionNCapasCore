using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Empresa : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAll(empresa);

            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Message = "Algo salioi mal";
            }
            return View(empresa);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.GetAll(empresa);

            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Message = "Algo salioi mal";
            }
            return View(empresa);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            if (IdEmpresa == null)
            {
                return View(empresa);
            }
            else
            {
                //Update
                ML.Result result = BL.Empresa.GetById(IdEmpresa.Value);

                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                }
                else
                {
                    ViewBag.Message = "No existe";
                }
            };
            return View(empresa);
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            IFormFile file = Request.Form.Files["IFImage"];
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                empresa.Logo = Convert.ToBase64String(ImagenBytes);
            }
            if (empresa.IdEmpresa == 0)
            {
                //Add
                
                result = BL.Empresa.Add(empresa);

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
                result = BL.Empresa.Update(empresa);

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

        [HttpGet]
        public ActionResult Delete(int? IdEmpresa)
        {
            ML.Result result = new ML.Result();

            if (IdEmpresa == null)
            {
                ViewBag.Message = "Algo ocurrio";
                return PartialView("Modal");
            }
            else
            {

                result = BL.Empresa.Delete(IdEmpresa.Value);
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

        public static byte[] ConvertToBytes(IFormFile Logo)
        {
            using var fileStream = Logo.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
