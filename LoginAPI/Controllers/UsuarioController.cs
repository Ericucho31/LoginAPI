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
using LoginAPI.Models;

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


        // METODO INSERTAR USUARIO ASIMETRICO//**********************************************************************************
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearUsuarioAsimetrico(InsertarUsuarioViewModel modelUsuario)
        {
            EncriptacionRSA rsa = new EncriptacionRSA();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'DBContextSistema.Usuarios'  is null.");
            }

            string PasswordEncriptada = rsa.Encriptacion(modelUsuario.Password);

            var email = modelUsuario.Email.ToUpper();
            if (await _context.Usuario.AnyAsync(u => u.Correo == email))
            {
                return BadRequest("El Email de este usuario ya existe"); //Función para validar que no se repita un Email
            }
            Usuario usuario = new Usuario
            {
                Correo = modelUsuario.Email,
                PasswordAsimetrico = PasswordEncriptada,
                LlavePublica = "5",
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
            return Ok(new { confirmacion = "Contrasena encriptada" + PasswordEncriptada });
        }

        private bool ComprobarPassword( string passwordIngresada, string passwordEncriptada, int privateKey)
        {
            EncriptacionRSA rsa = new EncriptacionRSA();
            if(passwordIngresada == rsa.Desencriptacion(passwordEncriptada, privateKey))
            {
                return true;
            }
            
            return false;
        }
        

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.Email.ToUpper();
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == email);

            if (usuario == null) { return NotFound(); }

            var IsValido = ComprobarPassword(model.Password, usuario.PasswordAsimetrico, model.PrivateKey);
            if (!IsValido) { return BadRequest("La contraseña o llave privada son incorrectas"); }

            return Ok(
                new { confirmacion = "Si eres tu" }
            );
        
        }


    }
}
