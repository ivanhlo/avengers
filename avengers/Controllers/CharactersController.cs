using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using avengers.Models;
using Newtonsoft.Json; // JsonConvert
using Newtonsoft.Json.Linq; // JObject

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
                 * Obteniendo información de la API de Marvel Comics para vaciarla en la BD Marvel
                 */
                int id_per;
                for (var i = 1; i < 2; i++)
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
                        int IdCom = result.id;
                        var ValidId = _context.Comics.Find(IdCom);
                        if (ValidId == null)
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
        public async Task<IEnumerable<Personaje>> GetCharacters()
        {
            return await _context.Personajes
                .Include(comics => comics.Comic)
                    .ThenInclude(creadores => creadores.Creador)
                .ToListAsync();
        }

        // GET marvel/Characters/{character}
        [HttpGet("{nom_per}")]
        public async Task<ActionResult<string>> GetCharacter(string nom_per)
        {
            /*
             * Selección y validación del personaje del cual se expondrá información
             */
            string NomPer;
            if (nom_per != "ironman" && nom_per != "capamerica")
                return NotFound();
            else if (nom_per == "ironman")
                NomPer = "Iron Man";
            else
                NomPer = "Captain America";
            /*
             * Consultas para filtrar la información que será expuesta
             */
            //obteniendo la fecha de la última sincronización
            var LastSync = await _context.Comics
                .Select(p => p.Last_sync)
                .Distinct()
                .ToListAsync();
            //obteniendo los id de los comics en que está involucrado el personaje
            var IdComics = await _context.Personajes
                .Where(b =>
                    b.Nom_per.Contains(NomPer))
                .Select(p => p.Id_com)
                .Distinct()
                .ToListAsync();
            //obteniendo lista de los otros heroes que interactúan con el personaje
            List<string> Personajes = new List<string>();
            foreach (var i in IdComics)
            {
                var ListaPer = await _context.Personajes
                    .Where(b =>
                        b.Id_com == i)
                    .Select(p => p.Nom_per)
                    .Distinct()
                    .ToListAsync();
                foreach (var j in ListaPer)
                {
                    Personajes.Add(j);
                }
            }
            List<string> Characters = Personajes.Distinct().ToList(); // lista de hereoes que interactuaron con el personaje sin ordenar
            
            //obteniendo detalle de los comics en que aparecen los otros heroes (sin descartar al personaje)
            JArray JCharacters = new JArray();
            JObject JChar = new JObject();
            foreach (var i in Characters)
            {
                var ListaIdCom = await _context.Personajes
                    .Where(b =>
                        b.Nom_per.Contains(i))
                    .Select(p => p.Id_com)
                    .Distinct()
                    .ToListAsync();
                List<string> Comics = new List<string>();
                foreach (var j in ListaIdCom)
                {
                    var ListaTitCom = await _context.Comics
                        .Where(b =>
                            b.Id == j)
                        .Select(p => p.Tit_com)
                        .ToListAsync();
                    Comics.AddRange(ListaTitCom);
                }
                JChar =
                    new JObject(
                        new JProperty("character", i),
                        new JProperty("comics", Comics));
                JCharacters.Add(JChar);
            }

            /*
             * Colección JSON final resultante
             */
            JObject rss =
                new JObject(
                    new JProperty("last_sync", LastSync[0]),
                    new JProperty("characters", JCharacters));

            return rss.ToString();
        }
    }
}
