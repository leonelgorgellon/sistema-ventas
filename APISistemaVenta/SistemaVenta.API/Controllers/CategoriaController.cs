using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet] //metodo
        [Route("Lista")] //añadimos la ruta para acceder a la api
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDTO>>(); //variable para nueva instancia para la clase response
 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _categoriaService.Lista();
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
