using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Errores;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ErrorTestController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ErrorTestController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundError()
        {
            var algo = _db.Lugar.Find(566);
            if(algo == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }   

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var algo = _db.Lugar.Find(566);
            var algoAretornar = algo.ToString();
            
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequestError()
        {
            return BadRequest( new ApiResponse(400));
        }  

         [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequestError(int id)
        {
            return BadRequest();
        }  
    }
}