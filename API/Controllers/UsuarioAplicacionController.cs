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
    public class UsuarioAplicacionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private readonly ILogger<UsuarioAplicacionController> _logger;
        private readonly IMapper _mapper;

        public UsuarioAplicacionController(ApplicationDbContext db, ILogger<UsuarioAplicacionController> logger,
        IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _db = db;
            _response = new ResponseDto();
        }

        
    }
}