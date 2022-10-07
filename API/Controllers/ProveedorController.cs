using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dto;
using Core.Entidades;
using Infraestructura.Data;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private ResponseDto _response;
        private readonly ILogger<ProveedorController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ProveedorController(IUnidadTrabajo unidadTrabajo, ILogger<ProveedorController> logger,
        IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _logger = logger;
            _response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            _logger.LogInformation("Listado de Proveedores");
            var lista = await _unidadTrabajo.Proveedor.ObtenerTodos();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Proveedores";
            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                return BadRequest(_response);

            }
            var proveedor = await _unidadTrabajo.Proveedor.ObtenerPrimero(p => p.Id == id);

            if (proveedor == null)
            {
                _logger.LogError("El Proveedor No Existe");
                _response.Mensaje = "El Proveedor No Existe";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            _response.Resultado = proveedor;
            _response.Mensaje = "Datos del proveedor " + proveedor.Id;
            return Ok(_response); //Status code = 200
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Proveedor>> PostProveedor([FromBody] ProveedorUpsertDto proveedorDto)
        {
            if (proveedorDto == null)
            {
                _response.Mensaje = "Información incorrecta";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proveedorExiste = await _unidadTrabajo.Proveedor.ObtenerPrimero
                                        (r => r.NombreProveedor.ToLower() == proveedorDto.NombreProveedor.ToLower());

            if (proveedorExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "Nombre del Proveedor ya existe!");
                return BadRequest(ModelState);
            }

            Proveedor proveedor = _mapper.Map<Proveedor>(proveedorDto);

            await _unidadTrabajo.Proveedor.Agregar(proveedor);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetProveedor", new { id = proveedor.Id }, proveedor); //Status code = 201
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutProveedor(int id, [FromBody] ProveedorUpsertDto proveedorDto)
        {
            if (id != proveedorDto.Id)
            {
                return BadRequest("Id de proveedor no coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proveedorExiste = await _unidadTrabajo.Proveedor.ObtenerPrimero(
                                               r => r.NombreProveedor.ToLower() == proveedorDto.NombreProveedor.ToLower()
                                               && r.Id != proveedorDto.Id);

            if (proveedorExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "El nombre del Proveedor ya existe");
            }

            Proveedor proveedor = _mapper.Map<Proveedor>(proveedorDto);

            _unidadTrabajo.Proveedor.Actualizar(proveedor);
            await _unidadTrabajo.Guardar();
            return Ok(proveedor);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _unidadTrabajo.Proveedor.ObtenerPrimero(p=>p.Id==id);
            if (proveedor == null)
            {
                return NotFound();
            }
            _unidadTrabajo.Proveedor.Remover(proveedor);
            await _unidadTrabajo.Guardar();
            return NoContent();
        }
    }
}