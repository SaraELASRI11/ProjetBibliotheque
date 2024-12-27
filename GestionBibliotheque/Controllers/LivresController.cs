using GestionBibliotheque.Models;
using GestionBibliotheque.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Bibliotheque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresController : ControllerBase
    {
        ILivreService service { get; set; }
        public LivresController(ILivreService service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult GetAllLivres()
        {
            ICollection<Livre> list = service.GetLivres();
            if (list.Count == 0)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetLivres(int id)
        {
            Livre livre = service.GetLivresById(id);
            if (livre == null)
            {
                return NotFound();
            }
            return Ok(livre);
        }
        [HttpGet("Titre")] 
        public IActionResult GetLivres([FromQuery] string titre)
        {
            ICollection<Livre> livres = service.GetLivresByTitle(titre);
            if (livres.Count == 0)
            {
                return NotFound();
            }
            return Ok(livres);
        }
        [HttpPost]
        public IActionResult PostLivre(Livre livres)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity();
            }
            service.Add(livres);
            return CreatedAtAction(nameof(GetLivres), new { id = livres.Id }, livres);
        }

        [HttpPut("{id}")]
        public IActionResult PutLivre(int id, Livre livres)
        {
            if (id != livres.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity();
            }
            service.Update(livres);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLivre(int id)
        {
            if (service.GetLivresById(id) == null)
            {
                return NotFound();
            }
            service.Remove(id);
            return NoContent();
        }

    }
}
