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
    public class VentaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private readonly ILogger<VentaController> _logger;
        private readonly IMapper _mapper;

        public VentaController(ApplicationDbContext db, ILogger<VentaController> logger,
        IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentaes()
        {
            _logger.LogInformation("Listado de Ventaes");
            var lista = await _db.Venta.ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Ventaes";
            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetVenta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                return BadRequest(_response);

            }
            var venta = await _db.Venta.FindAsync(id);

            if (venta == null)
            {
                _logger.LogError("El Venta No Existe");
                _response.Mensaje = "El Venta No Existe";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            _response.Resultado = venta;
            _response.Mensaje = "Datos del venta " + venta.Id;
            return Ok(_response); //Status code = 200
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Venta>> PostVenta([FromBody] VentaUpsertDto ventaDto)
        {
            if (ventaDto == null)
            {
                _response.Mensaje = "Información incorrecta";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ventaExiste = await _db.Venta.FirstOrDefaultAsync
                                        (r => r.Id == ventaDto.Id);

            if (ventaExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "Nombre del Venta ya existe!");
                return BadRequest(ModelState);
            }

            Venta venta= _mapper.Map<Venta>(ventaDto);

            await _db.Venta.AddAsync(venta);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVenta", new { id = venta.Id }, venta); //Status code = 201
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutVenta(int id, [FromBody] VentaUpsertDto ventaDto)
        {
            if (id != ventaDto.Id)
            {
                return BadRequest("Id de venta no coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ventaExiste = await _db.Venta.FirstOrDefaultAsync(
                                               r => r.Id == ventaDto.Id
                                               && r.Id != ventaDto.Id);

            if (ventaExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "El nombre del Venta ya existe");
            }

            Venta venta= _mapper.Map<Venta>(ventaDto);

            _db.Update(venta);
            await _db.SaveChangesAsync();
            return Ok(venta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVenta(int id)
        {
            var venta = await _db.Venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            _db.Venta.Remove(venta);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}