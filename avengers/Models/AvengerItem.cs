using System; // para la función DateTime
/*
 * Modelo de datos con EF Core
 */
namespace avengers.Models
{
    public class Comic
    {
        public int Id { get; set; }             // id para la tabla (obligatorio)
        public int Id_com { get; set; }         // guarda el id del cómic
        public int Id_per { get; set; }         // guarda el id del personaje del filtro (ironman o capamerica)
        public string Tit_com { get; set; }     // guarda el título del cómic
        public DateTime Last_sync { get; set; } // guarda la fecha y hora de sincronización
    }

    public class Creador
    {
        public int Id { get; set; }             // id para la tabla (obligatorio)
        public int Id_com { get; set; }         // guarda el id del cómic
        public int Id_per { get; set; }         // guarda el id del personaje del filtro (ironman o capamerica)
        public string Rol_cre { get; set; }     // guarda el rol del creador (colaborator)
        public string Nom_cre { get; set; }     // guarda el nombre del creador (colaborator)
    }

    public class Personaje
    {
        public int Id { get; set; }             // id para la tabla (obligatorio)
        public int Id_com { get; set; }         // guarda el id del cómic
        public int Id_per { get; set; }         // guarda el id del personaje del filtro (ironman o capamerica)
        public string Nom_per { get; set; }     // guarda el nombre del persona
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