using Microsoft.EntityFrameworkCore;
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

    }
}
