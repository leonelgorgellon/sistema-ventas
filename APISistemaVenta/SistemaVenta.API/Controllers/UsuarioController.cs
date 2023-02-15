using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }



        //DEVOLVEMOS LISTA DE USUARIO 
        [HttpGet] //metodo
        [Route("Lista")] //añadimos la ruta para acceder a la api
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<UsuarioDTO>>(); //variable para nueva instancia para la clase response

            //ejecutamos servicio 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _usuarioService.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }


        //INICIAMOS SESION 
        [HttpPost]
        [Route("IniciarSesion")]

        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login) //obtengo credenciales del usuario
        {
            var rsp = new Response<SesionDTO>(); //variable para nueva instancia para la clase response, creamos objeto de respuesta  

            //ejecutamos servicio, personalizamos los valores de la respuesta 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _usuarioService.ValidarCredenciales(login.Correo, login.Clave);
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }


        //GUARDAMOS USUARIO 
        [HttpPost]
        [Route("Guardar")]

        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario) //obtengo credenciales del usuario
        {
            var rsp = new Response<UsuarioDTO>(); //variable para nueva instancia para la clase response, creamos objeto de respuesta  

            //ejecutamos servicio, personalizamos los valores de la respuesta 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _usuarioService.Crear(usuario);
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }


        //EDITAMOS USUARIO 
        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario) 
        {
            var rsp = new Response<bool>(); 

            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _usuarioService.Editar(usuario);
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }


        //ELIMINAR USUARIO 
        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _usuarioService.Eliminar(id);
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
