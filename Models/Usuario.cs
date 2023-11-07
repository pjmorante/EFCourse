using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set;}
        //[RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Ingrese un correo valido")]
        [EmailAddress(ErrorMessage = "Ingrese un correo valido")]
        public string Email { get; set;}
        [Display(Name = "Dirección del Usuario")]
        public string Direccion { get; set;}
        [NotMapped]
        public int Edad { get; set; }
        [ForeignKey("DetalleUsuario")]
        public int? DetalleUsuario_Id { get; set; }
        public DetalleUsuario DetalleUsuario { get; set; }
    }
}
