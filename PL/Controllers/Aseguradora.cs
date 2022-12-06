using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Aseguradora : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Result result = BL.Aseguradora.GetAll();

            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            else
            {
                ViewBag.Message = "Algo salioi mal";
            }
            return View(aseguradora);
        }

        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();
            aseguradora.Usuario.ROL = new ML.Rol();
            ML.Result resultAseguradora = BL.Usuario.GetAll(aseguradora.Usuario);



            if (IdAseguradora == null)
            {
                aseguradora.Usuario.Usuarios = resultAseguradora.Objects;
                return View(aseguradora);
            }
            else
            {
                //Update
                ML.Result result = BL.Aseguradora.GetById(IdAseguradora.Value);

                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                }
                else
                {
                    ViewBag.Message = "No existe";
                }
            }
            aseguradora.Usuario.Usuarios = resultAseguradora.Objects;
            return View(aseguradora);
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            if (aseguradora.IdAseguradora == 0)
            {
                //Add
                ML.Result result = new ML.Result();
                result = BL.Aseguradora.Add(aseguradora);

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
                ML.Result result = BL.Aseguradora.Update(aseguradora);

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
        public ActionResult Delete(int? IdAseguradora)
        {
            ML.Result result = new ML.Result();

            if (IdAseguradora == null)
            {
                ViewBag.Message = "Algo ocurrio";
                return PartialView("Modal");
            }
            else
            {

                result = BL.Aseguradora.Delete(IdAseguradora.Value);
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
    }
}
