using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBackend.Data;
using WebBackend.Data.Entities;

namespace WebBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppEFContext _context;
        public CategoryController(AppEFContext appEFContext) =>_context = appEFContext; 
       
        [HttpGet]
        public async Task<IActionResult> GetCategorys()
        {
            List<CategoryEntity> categoryList = await  _context.Categories.ToListAsync();
            return Ok(categoryList);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryEntity category)
        {
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> EditCategory(CategoryEntity edit_category)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == edit_category.Id);
            if (category is not null)
            {
                category.Name = edit_category.Name;
                category.Image = edit_category.Image;
                _context.SaveChanges();

                return Ok();
            }
            else
                return NotFound();
            
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var contacts = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (contacts != null)
            {
                _context.Categories.Remove(contacts);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
      }
}
