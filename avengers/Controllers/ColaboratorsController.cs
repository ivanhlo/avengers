using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using avengers.Models;
using Newtonsoft.Json; // JsonConvert
using Newtonsoft.Json.Linq; // JObject

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
                 * Obteniendo información de la API de Marvel Comics para vaciarla en la BD Marvel
                 */
                int id_per;
                for (var i=1; i<2; i++)
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
        // GET: marvel/Colaborators
        [HttpGet]
        public async Task<ActionResult<List<Comic>>> GetColaborators()
        {
            return await _context.Comics
                .Include(creadores => creadores.Creador)
                .Include(personajes => personajes.Personaje)
                .ToListAsync();
        }

        // GET: marvel/Colaborators/{character}
        [HttpGet("{nom_per}")]
        public async Task<ActionResult<List<string>>> GetColaborator(string nom_per)
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
            var LastSync = await _context.Comics
                .Select(p => p.Last_sync)
                .Distinct()
                .ToListAsync();
            var IdComics = await _context.Personajes
                .Where(b =>
                    b.Nom_per.Contains(NomPer))
                .Select(p => p.Id_com)
                .Distinct()
                .ToListAsync();
            List<string> ListEditors = new List<string>();
            //string[] Editors = new string[0];
            List<string> Editors = new List<string>();
            //int c = 0;
            foreach (var i in IdComics)
            {
                ListEditors = await _context.Creadores
                    .Where(b =>
                        b.Rol_cre.Contains("editor") &&
                        b.Id_com == i)
                    .Select(p => p.Nom_cre)
                    .Distinct()
                    .ToListAsync();
                //Array.Resize(ref Editors, Editors.Length + ListEditors.Count());
                foreach (var j in ListEditors)
                {
                    Editors.Add(j);
                    //c++;
                }
            }
            
            return Editors.Distinct().ToList();
            /*
            var Writers = await _context.Creadores
                .Where(b =>
                    b.Rol_cre.Contains("writer")) //&&
                    //b.Id_per == id_per)
                .Select(p => p.Nom_cre)
                .Distinct()
                .ToListAsync();
            var Colorists = await _context.Creadores
                .Where(b =>
                    b.Rol_cre.Contains("colorist")) //&&
                    //b.Id_per == id_per)
                .Select(p => p.Nom_cre)
                .Distinct()
                .ToListAsync();
            /*
             * Colección JSON final resultante
             *//*
            JObject json =
                new JObject(
                    new JProperty("last_sync", LastSync[0]),
                    new JProperty("id_comics", ListEditors));
                    //new JProperty("editors", Editors),
                    //new JProperty("writers", Writers),
                    //new JProperty("colorists", Colorists));

            return json.ToString();*/
        }

        /*******************************************
         * t e r m i n a n   m é t o d o s   G E T *
         *******************************************/
    }
}
