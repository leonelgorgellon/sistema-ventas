using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios.Contratos
{
    public interface IGenericRepositorio<TModel> where TModel : class
    {
        //para obtener algun modelo. 
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);

        //recibe un modelo para crea un nuevo menu,categoria, etc 
        Task<TModel> Crear(TModel modelo);

        //editar modelo 
        Task<bool> Editar(TModel modelo);

        //eliminar un modelo 
        Task<bool> Eliminar(TModel modelo);

        //este nos va a devolver una consulta, osea la consutla del modelo, puede ser que podamos hacer un select a una tabla sin ninguna categoria por eso ponemos null el filtro
        Task<IQueryable<TModel>> Consultar (Expression<Func<TModel, bool>> filtro= null);



        
    }
}
