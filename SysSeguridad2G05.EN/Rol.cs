using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SysSeguridad2G05.EN
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del Rol  es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre no puede tener más de 30 caracteres.")]
        public string? Nombre { get; set; }
        public List<Usuario> Usuarios { get; set; } = null!;
        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
