using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Empresa : ControllerBase
    {
        // GET: api/<Empresa>
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAll(empresa);

            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
                return Ok(empresa);
            }
            else
            {
                return NotFound("Algo salioi mal");
            }
        }

        // GET api/<Empresa>/5
        [HttpGet("GetById")]
        public IActionResult GetById(int? idEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            if (idEmpresa == null)
            {
                return Ok(empresa);
            }
            else
            {
                //Update
                ML.Result result = BL.Empresa.GetById(idEmpresa.Value);

                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                    return Ok(empresa);
                }
                else
                {
                    return NotFound("No existe");
                }
            }
        }




        // POST api/<Empresa>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Empresa>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }




        // DELETE api/<Empresa>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? idEmpresa)
        {
            ML.Result result = new ML.Result();

            if (idEmpresa == null)
            {
                return NotFound("Algo Ocurrio");
            }
            else
            {

                result = BL.Empresa.Delete(idEmpresa.Value);
                if (result.Correct)
                {
                    return Ok("Se elimino correctamente");
                }

                else
                {
                    return NotFound("Algo Ocurrio");
                }
            }
        }
    }
}
