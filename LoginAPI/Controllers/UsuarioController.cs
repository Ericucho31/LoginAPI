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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text.Json;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly LoginContext _context;
        private readonly IConfiguration _configuracion;

        public UsuarioController(LoginContext context, IConfiguration configuracion)
        {
            _context = context;
            _configuracion = configuracion;
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

        // METODO CREAR PASSWORD ASIMÉTRICO//**********************************************************************************
        public static byte[] EncriptarConLlavePublica(string password, RSAParameters publicKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(publicKey);
                byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(password), RSAEncryptionPadding.OaepSHA256);
                return encryptedData;
            }
        }

        public static string Desencriptar(byte[] encryptedData, RSAParameters privateKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(privateKey);
                byte[] decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
                string decryptedPassword = Encoding.UTF8.GetString(decryptedData);
                return decryptedPassword;
            }
        }

        // METODO INSERTAR USUARIO ASIMETRICO//**********************************************************************************
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearUsuarioAsimetrico(InsertarUsuarioViewModel modelUsuario)
        {
            RSAParameters publicKey;
            RSAParameters privateKey;
            using (RSA rsa = RSA.Create())
            {
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'DBContextSistema.Usuarios'  is null.");
            }

            byte[] passwordEncriptado = EncriptarConLlavePublica( modelUsuario.Password, publicKey);
            byte[] publicKeyBytes = JsonSerializer.SerializeToUtf8Bytes(publicKey);

            var email = modelUsuario.Email.ToUpper();
            if (await _context.Usuario.AnyAsync(u => u.Correo == email))
            {
                return BadRequest("El Email de este usuario ya existe"); //Función para validar que no se repita un Email
            }
            Usuario usuario = new Usuario
            {
                Correo = modelUsuario.Email,
                PasswordAsimetrico = passwordEncriptado,
                LlavePublica = publicKeyBytes
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
            return Ok(new { confirmacion = "Esta es tu llave privada, no se la digas a nadie" + privateKey.ToString() } );
        }

        // METODO INSERTAR USUARIO//**********************************************************************************
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarUsuarios(InsertarUsuarioViewModel modelUsuario)
        {
            RSAParameters publicKey;
            RSAParameters privateKey;
            using (RSA rsa = RSA.Create())
            {
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }

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
                //PasswordHash = passwordHash,
                //PasswordSalt = passwordSalt
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
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == email);

            if (usuario == null) { return NotFound(); }

            //var IsValido = VerificaPassword(model.Password, usuario.PasswordHash, usuario.PasswordSalt);

            //if (!IsValido) { return BadRequest(); }

            return Ok(
                new { confirmacion = "Si eres tu" }
            );
        
        }


    }
}
