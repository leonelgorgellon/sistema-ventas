using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        //GUARDAMOS REGISTRO
        [HttpPost]
        [Route("Registrar")]

        public async Task<IActionResult> Registrar([FromBody] VentaDTO venta)
        {
            var rsp = new Response<VentaDTO>();

            //ejecutamos servicio, personalizamos los valores de la respuesta 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _ventaService.Registrar(venta);
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }


        //lista HISTORIAL VENTAS 
        [HttpGet] //metodo
        [Route("Historial")] //añadimos la ruta para acceder a la api
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<VentaDTO>>();

            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;


            //ejecutamos servicio 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _ventaService.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }

        //Lista REPORTE 
        [HttpGet] //metodo
        [Route("Reporte")] //añadimos la ruta para acceder a la api
        public async Task<IActionResult> Reporte( string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<ReporteDTO>>();

            //ejecutamos servicio 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _ventaService.Reporte( fechaInicio, fechaFin);
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
