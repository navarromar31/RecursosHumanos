using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Models;
namespace RecursosHumanos.Datos
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        //Aca iremos Creando basados en el modelo las respectivas tablas en la bd 


    }
}
