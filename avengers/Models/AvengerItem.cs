using System; // para la función DateTime
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*
 * Modelo de datos con EF Core
 */
namespace avengers.Models
{
    public class Comic
    {
        public Comic()
        {
            Personaje = new HashSet<Personaje>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }         // guarda el id del cómic
        public string Tit_com { get; set; }     // guarda el título del cómic
        public DateTime Last_sync { get; set; } // guarda la fecha y hora de sincronización

        public ICollection<Personaje> Personaje { get; set; }
        public ICollection<Creador> Creador { get; set; }
    }

    public class Creador
    {
        public int Id { get; set; }             // id para la tabla (obligatorio)
        public int Id_com { get; set; }         // guarda el id del cómic
        public string Nom_cre { get; set; }     // guarda el nombre del creador (colaborator)
        public string Rol_cre { get; set; }     // guarda el rol del creador (colaborator)

        public Comic Comic { get; set; }
    }

    public class Personaje
    {
        public int Id { get; set; }             // id para la tabla (obligatorio)
        public int Id_com { get; set; }         // guarda el id del cómic
        public string Nom_per { get; set; }     // guarda el nombre del persona

        public Comic Comic { get; set; }
    }
}

/*
 * Este es el Modelo de Datos basado en programación orientada a objetos, la cual se corresponderá
 * con la base de datos (BD) Marvel que será creada por medio de una migración llevada a cabo con
 * Entity Framework Core.
 * 
 * Cada clase dentro del namespace corresponderá a una tabla de la BD Marvel y se distingue
 * por una s al final del nombre de la clase, por lo que la tabla que le corresponderá a la clase
 * Creator se llamará Creators.
 */