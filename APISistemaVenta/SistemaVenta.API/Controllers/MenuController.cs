using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        //Lista MENU
        [HttpGet] //metodo
        [Route("Lista")] //añadimos la ruta para acceder a la api
        public async Task<IActionResult> Lista(int idUsuario)
        {
            var rsp = new Response<List<MenuDTO>>();

            //ejecutamos servicio 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _menuService.Lista(idUsuario);
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
