using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SysSeguridad2G05.EN
{
    public class Usuario
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        [ForeignKey("Rol")]
        [Required(ErrorMessage = "El Rol es obligatorio.")]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [StringLength(40, ErrorMessage= "Maximo 40 Caracteres")]
        [Display(Name = "Nombre Usuario")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(40, ErrorMessage = "Maximo 40 Caracteres")]
        [Display(Name = "Apellido Usuario")]
        public  string Apellido { get; set; }
        [Required(ErrorMessage = "Password es Obligatorio.")]
        [StringLength(40, ErrorMessage = "Maximo 100 Caracteres")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]   
        public string login { get; set; }
        [Required(ErrorMessage = "Password es Obligatorio.")]
        public string password { get; set; }
        [Display(Name = "Fecha Registro")]
       
        public DateTime FechaRegistro { get; set; }
        public byte Estatus { get; set; }
        public Rol Rol { get; set; }
        [NotMapped]
        public  int Top_Aux { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirmar Password es Obligatorio.")]
        [StringLength(40, ErrorMessage = "Maximo 100 Caracteres")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar Contraseña")]
       
        
        public string ConfirmarPassword_aux  { get; set; }

    }
    public  enum Estatus_Usuario
    {
        Activo = 1,
        Inactivo = 2,
        Suspendido = 3
    }
}
