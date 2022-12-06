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
                dependiente.Dependientes = result.Objects;
            }
            else
            {
                ViewBag.Message = "Algo salioi mal";
            }
            return View(dependiente);
        }
    }
}
