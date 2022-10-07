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
    public class RolController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private readonly ILogger<RolController> _logger;
        private readonly IMapper _mapper;

        public RolController(ApplicationDbContext db, ILogger<RolController> logger,
        IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
        {
            _logger.LogInformation("Listado de Roles");
            var lista = await _db.Rol.ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Roles";
            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                return BadRequest(_response);

            }
            var rol = await _db.Rol.FindAsync(id);

            if (rol == null)
            {
                _logger.LogError("El Rol No Existe");
                _response.Mensaje = "El Rol No Existe";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            _response.Resultado = rol;
            _response.Mensaje = "Datos del rol " + rol.Id;
            return Ok(_response); //Status code = 200
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rol>> PostRol([FromBody] RolDto rolDto)
        {
            if (rolDto == null)
            {
                _response.Mensaje = "Información incorrecta";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rolExiste = await _db.Rol.FirstOrDefaultAsync
                                        (r => r.NombreRol.ToLower() == rolDto.NombreRol.ToLower());

            if (rolExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "Nombre del Rol ya existe!");
                return BadRequest(ModelState);
            }

            Rol rol= _mapper.Map<Rol>(rolDto);

            await _db.Rol.AddAsync(rol);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetRol", new { id = rol.Id }, rol); //Status code = 201
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutRol(int id, [FromBody] RolDto rolDto)
        {
            if (id != rolDto.Id)
            {
                return BadRequest("Id de rol no coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rolExiste = await _db.Rol.FirstOrDefaultAsync(
                                               r => r.NombreRol.ToLower() == rolDto.NombreRol.ToLower()
                                               && r.Id != rolDto.Id);

            if (rolExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "El nombre del Rol ya existe");
            }

            Rol rol= _mapper.Map<Rol>(rolDto);

            _db.Update(rol);
            await _db.SaveChangesAsync();
            return Ok(rol);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteRol(int id)
        {
            var rol = await _db.Rol.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            _db.Rol.Remove(rol);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}