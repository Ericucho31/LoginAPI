using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Entidades
{
    public class Password
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPassword { get; set; }
        [Required]
        public byte[] PasswordEncriptada { get; set; }
        
        public int IdEmail { get; set; }
        [ForeignKey("IdEmail")]
        public virtual Email Email { get; set; }

    }
}
