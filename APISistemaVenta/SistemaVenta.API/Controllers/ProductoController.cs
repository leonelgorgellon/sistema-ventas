using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;


namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }


        //LISTAR PRODUCTO 
        [HttpGet] //metodo
        [Route("Lista")] //añadimos la ruta para acceder a la api
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ProductoDTO>>(); 

            //ejecutamos servicio 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _productoService.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }

        //GUARDAMOS PRODUCTO 
        [HttpPost]
        [Route("Guardar")]

        public async Task<IActionResult> Guardar([FromBody] ProductoDTO producto) 
        {
            var rsp = new Response<ProductoDTO>();

            //ejecutamos servicio, personalizamos los valores de la respuesta 
            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _productoService.Crear(producto);
            }
            catch (Exception ex)
            {
                rsp.status = false; //respuesta falsa 
                rsp.msj = ex.Message;
            }

            return Ok(rsp);
        }

        //EDITAMOS PRODUCTO 
        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] ProductoDTO producto)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true; //respuesta correcta
                rsp.value = await _productoService.Editar(producto);
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
                rsp.value = await _productoService.Eliminar(id);
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
