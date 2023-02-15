using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;

        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }


        //Lista RESUMEN 
        [HttpGet] //metodo
        [Route("Resumen")] //añadimos la ruta para acceder a la api
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashboardDTO>();

            //ejecutamos servicio 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _dashBoardService.Resumen();
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
