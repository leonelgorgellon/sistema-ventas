using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios.Contratos
{
    public interface IVentasRepositorio : IGenericRepositorio<Venta>
    {
        //esto funciona solamente para el modelo de venta.
        Task<Venta> Registrar(Venta modelo); 


    }
}
