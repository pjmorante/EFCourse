using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Models
{
    [Table("Tbl_Articulo")]
    public class Articulo
    {
        [Key]
        public int Articulo_Id { get; set; }
        [Column("Articulo")]
        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(20)]
        public string TituloArticulo { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "La descripción no debe superar los 20 caracteres")]
        public string Descripcion { get; set; }
        [Range(0.1, 5.0)]
        public double Calificacion { get; set; }
        [DataType(DataType.Date)]   
        public DateTime Fecha { get; set; }
        [ForeignKey("Categoria")]
        public int Categoria_Id { get; set; }
        public Categoria Categoria { get; set; }    
        public ICollection<ArticuloEtiqueta> ArticuloEtiqueta { get; set; } 
    }
}
