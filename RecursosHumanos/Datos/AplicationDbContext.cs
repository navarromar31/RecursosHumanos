using Microsoft.EntityFrameworkCore;
using ProyectoYo.Models;
using RecursosHumanos.Models;
using WebApplication1.Models;
namespace RecursosHumanos.Datos
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        //Aca iremos Creando basados en el modelo las respectivas tablas en la bd 

        public DbSet<Institucion> institucion { get; set; }
        public DbSet<Departamento> departamento { get; set; }
        public DbSet<Capacitacion> capacitacion { get; set; }
        public DbSet<Colaborador> colaborador { get; set; }
        public DbSet<Evaluacion> evaluacion { get; set; }
        public DbSet<Puesto> puesto { get; set; }
        public DbSet<Respuesta> respuesta { get; set; }
        public DbSet<Pregunta> pregunta { get; set; }
        public DbSet<EvaluacionColaborador> evaluacionColaborador { get; set; }

    }
}
