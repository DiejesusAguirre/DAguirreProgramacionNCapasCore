using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Dependiente : Controller
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
        public ActionResult GetAllDependientes(string NumeroEmpleado)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            //dependiente.Tipo = new ML.DependienteTipo();
            //dependiente.Civil = new ML.EstadoCivil();
            ML.Result result = BL.Dependiente.GetById(NumeroEmpleado);

            if (result.Correct)
            {
                dependiente = (ML.Dependiente)result.Object;
            }
            else
            {
                ViewBag.Message = "Algo salioi mal";
            }
            return View(dependiente);
        }
        [HttpGet]
        public ActionResult Form(int? IdDependiente)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.Civil = new ML.EstadoCivil();
            dependiente.Tipo = new ML.DependienteTipo();
            ML.Result resultCivil = BL.EstadoCivil.GetAll();
            ML.Result resultTipo = BL.DependienteTipo.GetAll();



            if (IdDependiente == null)
            {
                dependiente.Civil.EstadoCiviles = resultCivil.Objects;
                dependiente.Tipo.TiposDependientes = resultTipo.Objects;
                return View(dependiente);
            }
            else
            {
                //Update
                ML.Result result = BL.Dependiente.GetById(IdDependiente.Value);

                if (result.Correct)
                {
                    dependiente = (ML.Dependiente)result.Object;
                }
                else
                {
                    ViewBag.Message = "No existe";
                }
            }
            dependiente.Civil.EstadoCiviles = resultCivil.Objects;
            dependiente.Tipo.TiposDependientes = resultTipo.Objects;
            return View(dependiente);
        }
        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            if (dependiente.IdDependiente == 0)
            {
                //Add
                ML.Result result = new ML.Result();
                result = BL.Dependiente.Add(dependiente);

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
                ML.Result result = BL.Dependiente.Update(dependiente);

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
        public ActionResult Delete(int? IdDependiente)
        {
            ML.Result result = new ML.Result();

            if (IdDependiente == null)
            {
                ViewBag.Message = "Algo ocurrio";
                return PartialView("Modal");
            }
            else
            {

                result = BL.Dependiente.Delete(IdDependiente.Value);
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
