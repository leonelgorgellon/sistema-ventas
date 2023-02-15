using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepositorio : GenericRepositorio<Venta> , IVentasRepositorio
    {
        private readonly DbventaContext _dbContext;

        public VentaRepositorio(DbventaContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            //declaramos transacción. 
            using (var transaccion = _dbContext.Database.BeginTransaction())
            {
                //si dentro de la logica ocurre un error, tenemos q hacer que vuelva al principio.
                try
                {
                    //restamos el producto que esta involucrado en la venta
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        //buscamos el producto en la base de datos y lo restamos a la cantidad
                        Producto producto_encontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(producto_encontrado);
                    }

                    await _dbContext.SaveChangesAsync();

                    //generamos numero de documento
                    NumeroDocumento correlativo = _dbContext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now; //actualiza fecha de registro a la fecha actual 

                    _dbContext.NumeroDocumentos.Update(correlativo); //actualizamos información. 
                    await _dbContext.SaveChangesAsync();

                    //generamos formato de numero de venta 
                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    //00001
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    //actualizamos numero de documento de nuestra venta
                    modelo.NumeroDocumento = numeroVenta;

                    await _dbContext.Venta.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaccion.Commit(); //confirmamos la transaccion. 
                }
                catch
                {
                    transaccion.Rollback(); //restablece todo. 
                    throw;
                }

                return ventaGenerada;
            }
        }
    }
}
