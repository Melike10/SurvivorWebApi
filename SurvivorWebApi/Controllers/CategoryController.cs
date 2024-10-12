using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvivorWebApi.Context;
using SurvivorWebApi.DTO;
using SurvivorWebApi.Models;

namespace SurvivorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SurvivorContext _context;
        public CategoryController(SurvivorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok(_context.Categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = _context.Categories.First(c => c.Id == id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                CreateDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IsDeleted = false,
                // ilk olarak boş liste attık.
                Competitors = new List<Competitor>()
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CategoryDto category, int id)
        {
            var cat = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (cat is null)
                return NotFound();
          
            cat.Name = category.Name;
            cat.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoryById), new { id = cat.Id }, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null)
                return NotFound();
            category.IsDeleted = true;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);

        }

    }
}
