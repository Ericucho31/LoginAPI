using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models.Usuario
{
    public class InsertarUsuarioViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty; //Este dato es string porque se recibe desde ele frontend
    }
}
