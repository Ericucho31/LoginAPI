using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models.Usuario
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public int PrivateKey {  get; set; }
    }
}
