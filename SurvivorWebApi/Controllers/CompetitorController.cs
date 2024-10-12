using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurvivorWebApi.Context;
using SurvivorWebApi.DTO;
using SurvivorWebApi.Models;

namespace SurvivorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorController : ControllerBase
    {
        private readonly SurvivorContext _context;
        public CompetitorController( SurvivorContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public IActionResult GetAll() {
           return Ok( _context.Competitors);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comp = _context.Competitors.FirstOrDefault(x => x.Id == id);
            if (comp != null) 
                return NotFound();
            return Ok(comp);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var cat = await _context.Competitors.Where(c => c.CategoryId == categoryId).ToListAsync();
          

            return Ok(cat);
       }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompetitorDto competitorDto)
        {
            var competitor = new Competitor
            {
                   FirstName = competitorDto.FirstName,
                   LastName = competitorDto.LastName,
                   CreateDate=DateTime.Now,
                   ModifiedDate=DateTime.Now,
                   CategoryId = competitorDto.CategoryId,
                   IsDeleted=false
            };
            _context.Competitors.Add(competitor);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = competitor.Id }, competitor);
            }
            catch (Exception)
            {
                // Loglama yapabilirsiniz
                return StatusCode(500, "An error occurred while creating the competitor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompetitorDto competitorDto)
        {
            var comp = _context.Competitors.FirstOrDefault(c => c.Id == id);
            if (comp == null) { return NotFound(); }
             // Check if the provided CategoryId is valid
            var categoryExists = _context.Categories.Any(c => c.Id == competitorDto.CategoryId);
            if (!categoryExists)
              {
                 return BadRequest("Competitor must belong to a valid category.");
              }
            comp.FirstName = competitorDto.FirstName;
            comp.LastName = competitorDto.LastName;
            comp.ModifiedDate = DateTime.Now;
            comp.CategoryId = competitorDto.CategoryId;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = comp.Id }, comp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comp = _context.Competitors.FirstOrDefault(c => c.Id == id);
            if (comp == null) { return NotFound(); }
            comp.IsDeleted = true;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = comp.Id }, comp);
        }
        }
}
