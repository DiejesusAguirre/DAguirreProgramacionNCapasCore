using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Aseguradora : ControllerBase
    {
        // GET: api/<Aseguradora>
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Result result = BL.Aseguradora.GetAll();

            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
                return Ok(aseguradora);
            }
            else
            {
                return NotFound("Algo salioi mal");
            }
        }

        // GET api/<Aseguradora>/5
        [HttpGet("GetById")]
        public IActionResult GetAll(int? idAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();
            aseguradora.Usuario.ROL = new ML.Rol();
            ML.Result resultAseguradora = BL.Usuario.GetAll(aseguradora.Usuario);



            if (idAseguradora == null)
            {
                aseguradora.Usuario.Usuarios = resultAseguradora.Objects;
                return Ok(aseguradora);
            }
            else
            {
                //Update
                ML.Result result = BL.Aseguradora.GetById(idAseguradora.Value);

                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                    return Ok(aseguradora);
                }
                else
                {
                    return NotFound("No existe");
                }
            }
        }

        // POST api/<Aseguradora>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Add(aseguradora);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Algo ocurrio mal" + result.ErrorMessage);
            }
        }

        // PUT api/<Aseguradora>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            //result = BL.Usuario.Update(aseguradora);

            if (result.Correct == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Algo ocurrio mal" + result.ErrorMessage);
            }
        }

        // DELETE api/<Aseguradora>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? idAseguradora)
        {
            ML.Result result = new ML.Result();

            if (idAseguradora == null)
            {
                return NotFound("Algo Ocurrio");
            }
            else
            {

                result = BL.Aseguradora.Delete(idAseguradora.Value);
                if (result.Correct)
                {
                    return Ok("Se Elimino correctamente");
                }

                else
                {
                    return NotFound("No se puedo eliminar" + result.ErrorMessage);
                }
            }
        }
    }
}
