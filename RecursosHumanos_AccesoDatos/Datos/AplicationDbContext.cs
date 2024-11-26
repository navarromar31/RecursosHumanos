using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos_Models;
using RecursosHumanos_AccesoDatos;
using RecursosHumanos_AccesoDatos;
namespace RecursosHumanos_AccesoDatos.Datos
{
    public class AplicationDbContext : IdentityDbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {   

        }
        //Aca iremos Creando basados en el modelo las respectivas tablas en la bd 

        public DbSet<Institucion> instituciones { get; set; }
        public DbSet<Departamento> departamentos { get; set; }
        public DbSet<Capacitacion> capacitacion { get; set; }
        public DbSet<Colaborador> colaborador { get; set; }
        public DbSet<Evaluacion> evaluacion { get; set; }
        public DbSet<Puesto> puesto { get; set; }
        public DbSet<Respuesta> respuesta { get; set; }
        public DbSet<Pregunta> pregunta { get; set; }
        public DbSet<EvaluacionColaborador> evaluacionColaborador { get; set; }
        public DbSet<UsuarioAplicacion>UsuarioAplicacion { get; set; }
    }
}
