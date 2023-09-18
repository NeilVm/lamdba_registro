using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace registro.Models
{
    [Table("registro")]
    public class Registro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("prinom")]
        public string PrimerNombre { get; set; }

        [Column("segnom")]
        public string SegundoNombre { get; set; }

        [Required]
        [Column("apepater")]
        public string ApellidoPaterno { get; set; }

        [Column("apemater")]
        public string ApellidoMaterno { get; set; }

        [Required]
        [Column("correo")]
        public string Correo { get; set; }

        [Required]
        [Column("contrasena")]
        public string Contrasena { get; set; }

        [NotMapped] // No se mapea a la base de datos
        public string ConfirmarContrasena { get; set; }

        [Required]
        [Column("tipodoc")]
        public string TipoDocumento { get; set; }

        [Required]
        [Column("documento")]
        public string Documento { get; set; }

        [Column("fecnac")]
        public string FechaNacimiento { get; set; }

        [Required]
        [Column("sexo")]
        public string Sexo { get; set; }

        [Column("peso")]
        public string Peso { get; set; }

        [Column("alt")]
        public string Altura { get; set; }
    }
}
