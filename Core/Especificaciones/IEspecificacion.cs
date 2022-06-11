using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Especificaciones
{
    public interface IEspecificacion<T>
    {
        Expression<Func<T, bool >> Filtro {get;}
        List<Expression<Func<T, object >>> Includes {get;}
    }
}