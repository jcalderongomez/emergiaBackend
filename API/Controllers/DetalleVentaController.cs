using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dto;
using Core.Entidades;
using Infraestructura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private readonly ILogger<DetalleVentaController> _logger;
        private readonly IMapper _mapper;

        public DetalleVentaController(ApplicationDbContext db, ILogger<DetalleVentaController> logger,
        IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetalleVentaes()
        {
            _logger.LogInformation("Listado de DetalleVentaes");
            var lista = await _db.DetalleVenta.ToListAsync();
            _response.Resultado = _mapper.Map<IEnumerable<DetalleVenta>, IEnumerable<DetalleVentaReadDto>>(lista);
            _response.Mensaje = "Listado de DetalleVentaes";
            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetDetalleVenta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetalleVenta>> GetDetalleVenta(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                return BadRequest(_response);

            }
            var detalleVenta = await _db.DetalleVenta.FindAsync(id);

            if (detalleVenta == null)
            {
                _logger.LogError("El DetalleVenta No Existe");
                _response.Mensaje = "El DetalleVenta No Existe";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            _response.Resultado = detalleVenta;
            _response.Mensaje = "Datos del detalleVenta " + detalleVenta.Id;
            return Ok(_response); //Status code = 200
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleVenta>> PostDetalleVenta([FromBody] DetalleVentaUpsertDto detalleVentaDto)
        {
            if (detalleVentaDto == null)
            {
                _response.Mensaje = "Información incorrecta";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detalleVentaExiste = await _db.DetalleVenta.FirstOrDefaultAsync
                                        (r => r.Id == detalleVentaDto.Id);

            if (detalleVentaExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "Nombre del DetalleVenta ya existe!");
                return BadRequest(ModelState);
            }

            DetalleVenta detalleVenta= _mapper.Map<DetalleVenta>(detalleVentaDto);

            await _db.DetalleVenta.AddAsync(detalleVenta);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetDetalleVenta", new { id = detalleVenta.Id }, detalleVenta); //Status code = 201
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutDetalleVenta(int id, [FromBody] DetalleVentaUpsertDto detalleVentaDto)
        {
            if (id != detalleVentaDto.Id)
            {
                return BadRequest("Id de detalleVenta no coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detalleVentaExiste = await _db.DetalleVenta.FirstOrDefaultAsync(
                                               r => r.Id == detalleVentaDto.Id);

            if (detalleVentaExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "El nombre del DetalleVenta ya existe");
            }

            DetalleVenta detalleVenta= _mapper.Map<DetalleVenta>(detalleVentaDto);

            _db.Update(detalleVenta);
            await _db.SaveChangesAsync();
            return Ok(detalleVenta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteDetalleVenta(int id)
        {
            var detalleVenta = await _db.DetalleVenta.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            _db.DetalleVenta.Remove(detalleVenta);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}