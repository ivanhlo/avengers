using Microsoft.EntityFrameworkCore;

namespace avengers.Models
{
    public class AvengerContext : DbContext
    {
        public AvengerContext (DbContextOptions<AvengerContext> options)
            : base(options)
        {
        }

        public DbSet<Creator> AvengerItems { get; set; }
    }
}

/*
 * AvengerContext es el nombre de la clase Database Context para uso del ORM de Entity Framework (EF) Core
 * La clase DbContext pertenece a la biblioteca de EF Core
 * El método AvengerContext y la propiedad AvengerItems forman el constructor de la clase AvengerContext
 * Se pasa por medio del parámetro options una instancia del DbContextOptions del tipo AvengerContext
 * y a la clase base se pasan las opciones que provengan del parámetro options
 * básicamente esta es la manera estándar de configurar EF Core en un proyecto de ASP.NET Core
 * vamos a indicar también que la clase AvengerItem se va a corresponder con una tabla de la base de datos
 * para eso se coloca una propiedad del tipo DbSet, por lo que el nombre de la tabla que corresponde para
 * la clase AvengerItem se llamará AvengerItems (con una s al final).
 * Por lo tanto, las propiedades contenidas en la clase AvengerItem se convertirán en los campos de la
 * tabla AvengerItems
 */