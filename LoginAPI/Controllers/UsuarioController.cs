using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades;
using LoginAPI.Models.Usuario;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly LoginContext _context;

        public UsuarioController(LoginContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Usuario.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.IdUsuario == id);
        }

        // METODO CREAR PASSWORD//**********************************************************************************
        public static void CreaPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        
        // METODO INSERTAR USUARIO//**********************************************************************************
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarUsuarios(InsertarUsuarioViewModel modelUsuario)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'DBContextSistema.Usuarios'  is null.");
            }

            CreaPassword(modelUsuario.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var email = modelUsuario.Email.ToUpper();
            if (await _context.Usuario.AnyAsync(u => u.Correo == email))
            {
                return BadRequest("El Email de este usuario ya existe"); //Función para validar que no se repita un Email
            }
            Usuario usuario = new Usuario
            {
                Correo = modelUsuario.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _context.Usuario.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string Error = e.Message;
                var inner = e.InnerException;
                return BadRequest();
            }
            return Ok();
        }

        /*private string GeneraToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracion["Jwt:key"]));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuracion["Jwt:Issuer"],
                _configuracion["Jwt:Issuer"],
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credenciales,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerificaPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var nuevoPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHash).SequenceEqual(new ReadOnlySpan<byte>(nuevoPasswordHash));
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.Email.ToUpper();
            var usuario = await _context.Usuarios.Where(u => u.Estado == true).Include(u => u.IdRolNavigation).FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null) { return NotFound(); }

            var IsValido = VerificaPassword(model.Password, usuario.PasswordHash, usuario.PasswordSalt);

            if (!IsValido) { return BadRequest(); }

            var claim = new List<Claim>
            {
                //Claim utilizadas en el Backend
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, usuario.IdRolNavigation.NombreRol),

                //Claim utilizadas en el frontend
                new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim("Rol", usuario.IdRolNavigation.NombreRol),
                new Claim("NombreUsuario", usuario.NombreUsuario)
            };
            return Ok(
                new { token = GeneraToken(claim) }
            );
        
        }*/
    }
}
