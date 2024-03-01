using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmail { get; set; }
        [Required]
        public required string  Correo { get; set; }
        public virtual ICollection<Password> Passwords { get; set; } = new List<Password>();
    }
}
