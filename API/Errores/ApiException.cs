using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errores
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string mensaje = null, string detalle = null) : base(statusCode, mensaje)
        {
            Detalle = detalle;
        }

        public string Detalle { get; set; }
    }
}