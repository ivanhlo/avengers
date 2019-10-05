using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using avengers.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace avengers.Controllers
{
    [Route("marvel/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        /***************************************
         * i n i c i a   c o n s t r u c t o r *
         ***************************************/
        private readonly AvengerContext _context;

        public CharactersController(AvengerContext context)
        {
            _context = context;

            if (_context.Comics.Count() == 0 || _context.Creadores.Count() == 0 || _context.Personajes.Count() == 0)
            {
                /*
                 * Para efectos de prueba, se utiliza el constructor para crear un registro en cada tabla
                 * si la colection esta vacia, lo que significa que las tablas no se quedarán vacías.
                 */

                /*
                 * Obteniendo información de la API de Marvel Comics para vaciarla en la BD
                 * Lista de comics filtrada por el id de Iron Man
                 */
                int id_per;
                for (var i=1; i<=2; i++)
                {
                    if (i == 1)
                        id_per = 1009368; // Iron Man
                    else
                        id_per = 1009220; // Captain America
                    string url = "https://gateway.marvel.com:443/v1/public/characters/" + id_per + "/comics?ts=1&apikey=a90d074cc0483b65fe3c15a6c9970912&hash=792414c616577193fbe3817ba81822a8";
                    var JsonString = new WebClient().DownloadString(url);           // json como string
                    dynamic JsonObj = JsonConvert.DeserializeObject(JsonString);    // json como objeto
                    foreach (var result in JsonObj.data.results)
                    {
                        _context.Comics.Add(new Comic
                        {
                            Id = result.id,
                            Tit_com = result.title,
                            Last_sync = DateTime.Now
                        });
                        foreach (var resCreadores in result.creators.items)
                        {
                            _context.Creadores.Add(new Creador
                            {
                                Id_com = result.id,
                                Rol_cre = resCreadores.role,
                                Nom_cre = resCreadores.name
                            });
                        }
                        foreach (var resPersonajes in result.characters.items)
                        {
                            _context.Personajes.Add(new Personaje
                            {
                                Id_com = result.id,
                                Nom_per = resPersonajes.name
                            });
                        }
                    }
                }
                _context.SaveChanges();
            }
        }
        /*****************************************
         * t e r m i n a   c o n s t r u c t o r *
         *****************************************/

        /*****************************************
         * i n i c i a n   m é t o d o s   G E T *
         *****************************************/
        // GET: marvel/Characters
        [HttpGet]
        public IEnumerable<Personaje> GetCharacters()
        {
            return _context.Personajes.ToList();
        }

        // GET marvel/Characters/{character}
        [HttpGet("{nom_per}")]
        public async Task<ActionResult<List<String>>> GetCharacter(string nom_per)
        {
            /*
             * Selección y validación del personaje del cual se expondrá información
             */
            int id_per;
            if (nom_per != "ironman" && nom_per != "capamerica")
                return NotFound();
            else if (nom_per == "ironman")
                id_per = 1009368;
            else
                id_per = 1009220;
            /*
             * Consultas para filtrar la información que será expuesta
             * SELECT DISTINCT personajes.nom_per, comics.tit_com
             * FROM (comics INNER JOIN creadores ON comics.id_com = creadores.id_com) INNER JOIN personajes ON comics.id_com = personajes.id_com
             * WHERE (((personajes.nom_per)<>"Iron Man") AND ((personajes.id_per)=1009368))
             * ORDER BY personajes.nom_per;
             */
            var LastSync = await _context.Comics
                .Where(b =>
                    b.Id == id_per)
                .Select(p => p.Last_sync)
                .Distinct()
                .ToListAsync();
            var Editores = await _context.Creadores
                .Where(b =>
                    b.Rol_cre.Contains("editor")) //&&
                    //b.Id_per == 1009368)
                .Select(p => p.Nom_cre)
                .Distinct()
                .ToListAsync();




            /*
             * Colección JSON final resultante
             */



            return Editores;
        }
    }
}
