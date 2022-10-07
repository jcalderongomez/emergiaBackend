using System.Collections;
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
using Infraestructura.Data.Repositorio.IRepositorio;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private ResponseDto _response;
        private readonly ILogger<ProductoController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ProductoController(IUnidadTrabajo unidadTrabajo, ILogger<ProductoController> logger,
        IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _logger = logger;
            _response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductoReadDto>>> GetProductos()
        {
            _logger.LogInformation("Listado de Productos");
            var lista = await _unidadTrabajo.Producto.ObtenerTodos();
            _response.Resultado = _mapper.Map<IEnumerable<Producto>, IEnumerable<ProductoReadDto>>(lista);
            _response.Mensaje = "Listado de Productos";
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);

            }
            var producto = await _unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == id);

            if (producto == null)
            {
                _logger.LogError("El Producto No Existe");
                _response.Mensaje = "El Producto No Existe";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Resultado = _mapper.Map<Producto, ProductoReadDto>(producto);
            _response.Mensaje = "Datos del producto " + producto.Id;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response); //Status code = 200
        }

        [HttpGet]
        [Route("ProductosPorProveedor/{proveedorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductoReadDto>>> GetProductosPorProveedor(int proveedorId)
        {
            _logger.LogInformation("Listado de Productos por Proveedor");
            var lista = await _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades: "Proveedor");
            _response.Resultado = _mapper.Map<IEnumerable<Producto>, IEnumerable<ProductoReadDto>>(lista);
            _response.IsExitoso = true;
            _response.Mensaje = "Listado de Productos por Proveedor";
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> PostProducto([FromBody] ProductoUpsertDto productoDto)
        {
            if (productoDto == null)
            {
                _response.Mensaje = "Información incorrecta";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return NotFound(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productoExiste = await _unidadTrabajo.Producto.ObtenerPrimero(
                r => r.Descripcion.ToLower() == productoDto.Descripcion.ToLower());


            if (productoExiste != null)
            {
                //ModelState.AddModelError("NombreDuplicado", "Nombre del Producto ya existe!");
                _response.IsExitoso = false;
                _response.Mensaje = "Nombre del Producto ya existe!!";
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Producto producto = _mapper.Map<Producto>(productoDto);

            await _unidadTrabajo.Producto.Agregar(producto);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso = true;
            _response.Mensaje = "Producto Guardado con Exito";
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetProducto", new { id = producto.Id }, producto); //Status code = 201
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutProducto(int id, [FromBody] ProductoUpsertDto productoDto)
        {
            if (id != productoDto.Id)
            {
                _response.IsExitoso = false;
                _response.Mensaje = "Id del Prodcuto no coincide";
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest("Id de producto no coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productoExiste = await _unidadTrabajo.Producto.ObtenerPrimero(
                                               r => r.Descripcion.ToLower() == productoDto.Descripcion.ToLower()
                                               && r.Id != productoDto.Id);

            if (productoExiste != null)
            {
                //ModelState.AddModelError("NombreDuplicado", "El nombre del Producto ya existe");
                _response.IsExitoso = true;
                _response.Mensaje = "El nombre del producto ya existe";

            }

            Producto producto = _mapper.Map<Producto>(productoDto);

            _unidadTrabajo.Producto.Actualizar(producto);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso = true;
            _response.Mensaje = "Producto Actualizado";
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            var producto = await _unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == id);
            if (producto == null)
            {
                _response.IsExitoso = false;
                _response.Mensaje = "Producto no existe";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound();
            }
            _unidadTrabajo.Producto.Remover(producto);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso = true;
            _response.Mensaje = "Producto eliminado";
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}