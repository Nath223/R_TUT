using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R_TUT.Models
{
    [Table("DOCUMENTOS")]
    public class DOCUMENTOS
    {
        [Key]  // ✅ Llave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // ✅ Autoincremental
        public int Id { get; set; }
        [Required]
        public string Usuario { get; set; } = string.Empty;
        [Required]
        public int Cuatrimestre { get; set; }
        [Required]
        public string Materia { get; set; } = string.Empty;
        [Required]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        public string Descripcion { get; set; } = string.Empty;
        public string Bibliografia { get; set; } = string.Empty;
        [Required]
        public required byte[] Documento { get; set; }
    }
}
