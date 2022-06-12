using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errores
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string mensaje = null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? MensajeStatusCode(statusCode);
        }
        public int StatusCode { get; set; }
        public string Mensaje { get; set; }

        private string MensajeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Un Bad Request se ha realizado",
                401 => "No estas autorizado",
                404 => "Recurso no encontrado",
                500 => "Error interno, comunicarse con el administrador",
                _ => null
            };
        }
    }
}