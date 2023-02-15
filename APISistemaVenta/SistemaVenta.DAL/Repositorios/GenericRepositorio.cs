using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DAL.DBContext;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SistemaVenta.DAL.Repositorios
{
    public class GenericRepositorio<TModelo> : IGenericRepositorio<TModelo> where TModelo : class
    {
        private readonly DbventaContext _dbContext;

        public GenericRepositorio(DbventaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                //devolvemos el primero y en caso de no encontrarlo enviamo el valor de null 
                TModelo modelo = await _dbContext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo; 
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                //lo primero aca es establecer el modelo con el que vamos a trabjar _dbContext.Set<TModelo>()
                _dbContext.Set<TModelo>().Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo; 
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _dbContext.Set<TModelo>().Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {

                _dbContext.Set<TModelo>().Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {
                //devolvemos la consulta para quien lo llame sea quien lo ejecute. 
                //validamos filtro si es igual a null, 
                //luego devolvemos el modelo por el cual esta haciendo la consulta. 
                //caso contrario que devovlio algun filtro, devolvemos el modelo y luego especificamos el Where y ponemos el filtro 
                IQueryable<TModelo> queryModelo = filtro == null ? _dbContext.Set<TModelo>() : _dbContext.Set<TModelo>().Where(filtro);
                return queryModelo;
            }
            catch
            {
                throw;
            }
        }

        
    }
}
