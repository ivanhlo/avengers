namespace avengers.Models
{
    public class Creator
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

/*
 * Este es el Modelo de Datos basado en programación orientada a objetos, la cual se corresponderá
 * con la tabla AvengerItems que será creada por medio de una migración llevada a cabo con
 * Entity Framework Core.
 * 
 * Cada clase dentro del namespace corresponderá a una tabla de la base de datos y se distingue
 * por una s al final del nombre de la clase, por lo que la tabla que le corresponderá a la clase
 * AvengerItem se llamará AvengerItems.
 */