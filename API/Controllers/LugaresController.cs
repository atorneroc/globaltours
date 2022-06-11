using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using AutoMapper;
using Core.Entidades;
using Core.Especificaciones;
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
        public IRepositorio<Lugar> _lugarRepo { get; set; }
        public IRepositorio<Pais> _paisRepo { get; }
        private readonly IRepositorio<Categoria> _categoriaRepo;
        private readonly IMapper _mapper;
        
        public LugaresController(IRepositorio<Lugar> lugarRepo, IRepositorio<Pais> paisRepo, IRepositorio<Categoria> categoriaRepo,
                                IMapper mapper)
        {
            _mapper = mapper;
            _categoriaRepo = categoriaRepo;
            _paisRepo = paisRepo;
            _lugarRepo = lugarRepo;            
        }

        [HttpGet]
       public async Task< ActionResult<IReadOnlyList<LugarDto>>> GetLugares()
       {
           var espec = new LugaresConPaisCategoriaEspecificacion();
           var lugares = await _lugarRepo.ObtenerTodosEspec(espec);

           return Ok(_mapper.Map<IReadOnlyList<Lugar>, IReadOnlyList<LugarDto>>(lugares));
       }

       [HttpGet("{id}")]
       public async Task<ActionResult<LugarDto>> GetLugar(int id)
       {
           var espec = new LugaresConPaisCategoriaEspecificacion(id);
           var lugar = await _lugarRepo.ObtenerEspec(espec);
           return _mapper.Map<Lugar,LugarDto>(lugar);
       }

       [HttpGet("paises")]
       public async Task <ActionResult<List<Pais>>> GetPaises()
       {
           return Ok(await _paisRepo.ObtenerTodosAsync());
       }

       [HttpGet("categoria")]
       public async Task <ActionResult<List<Categoria>>> GetCategorias()
       {
           return Ok(await _categoriaRepo.ObtenerTodosAsync());
       }

    }
}