using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using avengers.Models;

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

                _context.Comics.Add(new Comic
                {
                    Id_com = 75181,
                    Id_per = 1009368,
                    Tit_com = "Decades: Marvel in The '80s - Awesome Evolutions (Trade Paperback)",
                    Last_sync = DateTime.Today
                });
                _context.Creadores.Add(new Creador
                {
                    Id_com = 75181,
                    Id_per = 1009368,
                    Rol_cre = "penciller",
                    Nom_cre = "various"
                });
                _context.Personajes.Add(new Personaje
                {
                    Id_com = 75181,
                    Id_per = 1009368,
                    Nom_per = "Captain America"
                });
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
        public async Task<ActionResult<IEnumerable<Personaje>>> GetAvengerItems()
        {
            return await _context.Personajes.ToListAsync();
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
