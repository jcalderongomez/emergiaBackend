using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Data.Repositorio
{
    public class UsuarioAplicacionRepositorio : Repositorio<UsuarioAplicacion>, IUsuarioAplicacionRepositorio
    {
        private readonly ApplicationDbContext _db;

        public UsuarioAplicacionRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<string> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Register(UsuarioAplicacion usuarioAplicacion, string password)
        {
            try
            {
                if(await UserExiste(usuarioAplicacion.UserApp))
                {
                    return -1;
                }
                CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                usuarioAplicacion.PasswordHash = passwordHash;
                usuarioAplicacion.PasswordSalt = passwordSalt;

                await _db.UsuarioAplicacion.AddAsync(usuarioAplicacion);
                await _db.SaveChangesAsync();
                return usuarioAplicacion.Id;
            }
            catch (Exception)
            {
                return -500;
            }
        }

        public async Task<bool> UserExiste(string userName)
        {
            if (await _db.UsuarioAplicacion.AnyAsync(x => x.UserApp.ToLower().Equals(userName.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}