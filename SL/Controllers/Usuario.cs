using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario : ControllerBase
    {
        // GET: api/<Usuario>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.ROL = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);
            ML.Result resultRol = BL.Rol.GetAll();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.ROL.Roles = resultRol.Objects;
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetById/{idUsuario}")]
        public IActionResult GetById(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            usuario.ROL = new ML.Rol();
            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();

            if (idUsuario == null)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.ROL.Roles = resultRol.Objects;
                return Ok(usuario);
            }
            else
            {
                //Update
                ML.Result result = new ML.Result();
                    result =BL.Usuario.GetById(idUsuario);

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
                    return Ok(result);

                }
                else
                {
                    return NotFound();
                }
                return NotFound();
            }
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(string? nombre, string? apellidoPaterno, int? idRol)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.ROL = new ML.Rol();
            usuario.Nombre = (nombre == null) ? "" : nombre;
            usuario.ApellidoPaterno = (apellidoPaterno == null) ? "" : apellidoPaterno;
            usuario.ROL.IdRol = (idRol == null) ? 0 : idRol;


            usuario.ROL = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);
            ML.Result resultRol = BL.Rol.GetAll();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.ROL.Roles = resultRol.Objects;
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<Usuario>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Algo ocurrio mal" + result.ErrorMessage);
            }
        }

        // PUT api/<Usuario>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.Update(usuario);

            if (result.Correct == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Algo ocurrio mal" + result.ErrorMessage);
            }
        }

        // DELETE api/<Usuario>/5
        [HttpDelete("Delete/{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();

            if (idUsuario == null)
            {
                return NotFound("Algo salio mal");
            }
            else
            {

                result = BL.Usuario.Delete(idUsuario);
                if (result.Correct)
                {
                    return Ok("Se elimino correctamente");
                }

                else
                {
                    return NotFound();
                }
            }
        }
    }
}
