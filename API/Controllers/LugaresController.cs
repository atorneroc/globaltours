using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        public ILugarRepositorio _repo { get; }
        
        public LugaresController(ILugarRepositorio repo)
        {
            _repo = repo;
            
        }

        [HttpGet]
       public async Task< ActionResult<List<Lugar>>> GetLugares()
       {
           var lugares = await _repo.GetLugaresAsync();

           return Ok(lugares);
       }

       [HttpGet("{id}")]
       public async Task<ActionResult<Lugar>> GetLugar(int id)
       {
           return await _repo.GetLugarAsync(id);
       }
    }
}