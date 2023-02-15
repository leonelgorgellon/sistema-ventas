using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IVentasRepositorio _ventaRepositorio;
        private readonly IGenericRepositorio<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public DashBoardService(IVentasRepositorio ventaRepositorio, IGenericRepositorio<Producto> productoRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        //creamos metodo de acceso solo para esta clase 
        //objetivo es devolver todo un rango de ventas de acuerdo a una fecha que indiquemos. 
        private IQueryable<Venta> RetornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVentas(_ventaQuery, -7);
                total = tablaVenta.Count();
            }

            return total;
        }

        private async Task<string> TotalIngresoUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaventa = RetornarVentas(_ventaQuery, -7);

                resultado = tablaventa.Select(v => v.Total).Sum(v => v.Value);

            }

            return Convert.ToString(resultado, new CultureInfo("es-AR"));
                
        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> _productoQuery = await _productoRepositorio.Consultar();

            int total = _productoQuery.Count();
            return total;

        }

        private async Task<Dictionary<string,int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();
            
            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVentas(_ventaQuery, -7);

                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key) //agrupamos y ordenamos por columna 
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            return resultado;

        }

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashBoard = new DashboardDTO();

            try
            {
                vmDashBoard.TotalVentas = await TotalVentasUltimaSemana();
                vmDashBoard.TotalIngresos = await TotalIngresoUltimaSemana();
                vmDashBoard.TotalProductos = await TotalProductos();

                List<VentaSemanaDTO> listaVentaSemana = new List<VentaSemanaDTO>();

                foreach(KeyValuePair<string,int> item in await VentasUltimaSemana())
                {
                    listaVentaSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                vmDashBoard.VentasUltimaSemana = listaVentaSemana;
            }
            catch
            {
                throw;
            }

            return vmDashBoard;
        }
    }
}
