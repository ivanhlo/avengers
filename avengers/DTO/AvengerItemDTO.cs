using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace avengers.DTO
{
    public class ComicDTO
    {
        public int Id { get; set; }         // guarda el id del cómic
        public string Tit_com { get; set; }     // guarda el título del cómic
        public DateTime Last_sync { get; set; } // guarda la fecha y hora de sincronización

        public List<PersonajeDTO> Personaje { get; set; }
        public List<CreadorDTO> Creador { get; set; }
    }

    public class CreadorDTO
    {
        public int Id { get; set; }             // id para la tabla (obligatorio)
        public int Id_com { get; set; }         // guarda el id del cómic
        public string Nom_cre { get; set; }     // guarda el nombre del creador (colaborator)
        public string Rol_cre { get; set; }     // guarda el rol del creador (colaborator)

        public ComicDTO Comic { get; set; }
    }

    public class PersonajeDTO
    {
        public int Id { get; set; }             // id para la tabla (obligatorio)
        public int Id_com { get; set; }         // guarda el id del cómic
        public string Nom_per { get; set; }     // guarda el nombre del persona

        public ComicDTO Comic { get; set; }
    }

}
