using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using avengers.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace avengers.Controllers
{
    [Route("marvel/[controller]")]
    [ApiController]
    public class ColaboratorsController : ControllerBase
    {
        /***************************************
         * i n i c i a   c o n s t r u c t o r *
         ***************************************/
        private readonly AvengerContext _context;

        public ColaboratorsController(AvengerContext context)
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
                string url = "https://gateway.marvel.com:443/v1/public/characters/1009368/comics?ts=1&apikey=a90d074cc0483b65fe3c15a6c9970912&hash=792414c616577193fbe3817ba81822a8";
                var JsonString = new WebClient().DownloadString(url);           // json como string
                dynamic JsonObj = JsonConvert.DeserializeObject(JsonString);    // json como objeto
                foreach(var result in JsonObj.data.results)
                {
                    _context.Comics.Add(new Comic
                    {
                        Id_com = result.id, //75181,
                        Id_per = 1009368,
                        Tit_com = result.title, //"Decades: Marvel in The '80s - Awesome Evolutions (Trade Paperback)",
                        Last_sync = DateTime.Now
                    });
                    foreach (var resCreadores in result.creators.items)
                    {
                        _context.Creadores.Add(new Creador
                        {
                            Id_com = result.id, //75181,
                            Id_per = 1009368,
                            Rol_cre = resCreadores.role, //"penciller",
                            Nom_cre = resCreadores.name //"various"
                        });
                    }
                    foreach(var resPersonajes in result.characters.items)
                    {
                        _context.Personajes.Add(new Personaje
                        {
                            Id_com = result.id, //75181,
                            Id_per = 1009368,
                            Nom_per = resPersonajes.name //"Captain America"
                        });
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
        // GET: marvel/Colaborators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creador>>> GetAvengerItems()
        {
            return await _context.Creadores.ToListAsync();
        }

        // GET: marvel/Colaborators/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>> GetCharacterById(int id)
        {
            var characterItem = await _context.Personajes.FindAsync(id);

            if (characterItem == null)
            {
                return NotFound();
            }

            return characterItem;
        }

        // GET: marvel/colaborators/ironman
        /*[HttpGet("{name}")]
        public async Task<ActionResult<AvengerItem>> GetCharacterByName(string name)
        {
            var characterItem = await _context.AvengerItems.FindAsync(name);

            if (characterItem == null)
            {
                return NotFound();
            }

            return characterItem;
        }*/
        /*******************************************
         * t e r m i n a n   m é t o d o s   G E T *
         *******************************************/
    }
}
