using ASP_RadhitiaPratama.DTOS;
using ASP_RadhitiaPratama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_RadhitiaPratama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(EsemkaStoreContext context) : ControllerBase
    {
        private readonly EsemkaStoreContext _context = context;

        [HttpGet("/Category")]
        public ActionResult All()
        {
            var query = _context.Categories.ToList();
            return Ok(query);
        }


        [HttpGet("/Category/{id}")]
        public ActionResult All(int id)
        {
            var query = _context.Categories.Where(f => f.Id == id).FirstOrDefault();
            if (query == null) return NotFound();
            return Ok(query);
        }


        [HttpPost("/Category")]
        public ActionResult Create(CategoryDTOInsert categoryDTO)
        {
            Category newCategory = new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            return Ok(categoryDTO);
        }

        [HttpPut("/Category/{id}")]
        public ActionResult Update(int id, CategoryDTOInsert categoryDTO)
        {
            var oldCategory = _context.Categories.Where(f => f.Id == id).FirstOrDefault();
            if (oldCategory == null) return NotFound();

            oldCategory.Name = categoryDTO.Name;
            oldCategory.Description = categoryDTO.Description;

            _context.SaveChanges();
            return Ok(categoryDTO);
        }

        [HttpDelete("/Category/{id}")]
        public ActionResult Delete(int id)
        {
            var oldCategory = _context.Categories.Where(f => f.Id == id).FirstOrDefault();
            if (oldCategory == null) return NotFound();

            try
            {
                _context.Categories.Remove(oldCategory);
                _context.SaveChanges();
                return Ok(oldCategory);
            }
            catch (Exception ex)
            {
                _context.Entry(oldCategory).Reload();
                return StatusCode(500);
            }
        }
    }
}
