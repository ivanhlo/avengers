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

            if (_context.AvengerItems.Count() == 0)
            {
                // Crea un nuevo TodoItem si la colection esta vacia, lo que
                // significa que no se pueden eliminar todos los TodoItems.
                _context.AvengerItems.Add(new AvengerItem { Name = "capamerica" });
                _context.AvengerItems.Add(new AvengerItem { Name = "ironman" });
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
        public async Task<ActionResult<IEnumerable<AvengerItem>>> GetAvengerItems()
        {
            return await _context.AvengerItems.ToListAsync();
        }

        // GET: marvel/Colaborators/ironman
        [HttpGet("{id}")]
        public async Task<ActionResult<AvengerItem>> GetCharacterItem(long id)
        {
            var characterItem = await _context.AvengerItems.FindAsync(id);

            if (characterItem == null)
            {
                return NotFound();
            }

            return characterItem;
        }

        /*******************************************
         * t e r m i n a n   m é t o d o s   G E T *
         *******************************************/
    }
}
