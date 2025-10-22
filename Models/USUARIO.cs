using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace R_TUT.Models
{
    [Table("USUARIO")]
    public class USUARIO
    {
        [Key]  // ✅ Llave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // ✅ Autoincremental
        public int Id_USUARIO { get; set; }
        [Required]  // ✅ No nulo
        public string Nombre { get; set; } = string.Empty;
        [Required]  // ✅ No nulo
        public int Cuatrimestre { get; set; }
        [Required]
        public int Matricula { get; set; }
        [Required]
        public string N_Usuario { get; set; } = string.Empty;
        [Required]
        public string Contrasena { get; set; } = string.Empty;
    }
}
