using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolServicio;

        public RolController(IRolService rolServicio)
        {
            _rolServicio = rolServicio;
        }

        [HttpGet] //metodo
        [Route("Lista")] //añadimos la ruta para acceder a la api
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<RolDTO>>(); //variable para nueva instancia para la clase response

            //ejecutamos servicio 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _rolServicio.Lista();
            }
            catch(Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
