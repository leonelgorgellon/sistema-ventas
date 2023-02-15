using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DAL.Repositorios;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.BLL.Servicios;

using SistemaVenta.Utility;

namespace SistemaVenta.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration  configuration)
        {
            services.AddDbContext<DbventaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            //creamos la dependencia de los repositorios que creamos. 
            services.AddTransient(typeof(IGenericRepositorio<>), typeof(GenericRepositorio<>));

            //dependencia para las ventas 
            services.AddScoped<IVentasRepositorio, VentaRepositorio>();

            //agregamos la referencia de automapper con todos los mapeos. 
            services.AddAutoMapper(typeof(AutoMapperProfile));


            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IVentaService, VentaService>();

        }
    }
}
