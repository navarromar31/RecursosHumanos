using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RecursosHumanos_Models
{
    public class Colaborador
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "La cedula del colaborador es obligatorio")]
        public string? CedulaColaborador { get; set; }

        [Required(ErrorMessage = "El nombre del colaborador es obligatorio")]
        public string NombreColaborador { get; set; }

        [Required(ErrorMessage = "El apellido del colaborador es obligatorio")]
        public string Apellido1 { get; set; }

        [Required(ErrorMessage = "El apellido del colaborador es obligatorio")]
        public string Apellido2 { get; set; }

        [Required(ErrorMessage = "El correo del colaborador es obligatorio")]
        public string CorreoColaborador { get; set; }
        public string? ImagenUrlCol { get; set; }
        public bool? EstadoColaborador { get; set; }

        [Required(ErrorMessage = "El colaborador debe ser asignado a un puesto obligatoriamente")]
        public int IdPuesto { get; set; }
        [ForeignKey("PuestoId")]
        public virtual Puesto? Puesto { get; set; }

        [Required(ErrorMessage = "El colaborador debe ser asignado a una institución obligatoriamente")]
        public int IdInstitucion { get; set; }
        [ForeignKey("InstitucionId")]
        public virtual Institucion? Institucion { get; set; }

        [Required(ErrorMessage = "El colaborador debe estar asignado a un departamento obligatoriamente")]
        public int IdDepartamento { get; set; }
        [ForeignKey("DepartamentoId")]
        public virtual Departamento? Departamento { get; set; }

    }
}
