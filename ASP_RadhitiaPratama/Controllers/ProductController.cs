using ASP_RadhitiaPratama.DTOS;
using ASP_RadhitiaPratama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_RadhitiaPratama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(EsemkaStoreContext context) : ControllerBase
    {
        private readonly EsemkaStoreContext _context = context;

        [HttpGet("/Product")]
        public ActionResult All([FromQuery] int? categoryID, [FromQuery] string? search)
        {
            var query = _context.Products.AsQueryable();
            if (categoryID != null)
            {
                query = query.Where(f => f.CategoryId == categoryID);
            }

            if (search != null)
            {
                query = query.Where(f => f.Name.Contains(search));
            }


            return Ok(query.ToList());
        }

        [HttpGet("/Product/{id}")]
        public ActionResult GetByID(int id)
        {
            var query = _context.Products.Where(f => f.Id == id).ToList();
            return Ok(query);
        }


        [HttpPost("/Product")]
        public ActionResult Create(ProductDTOInsert productDTO)
        {
            Product newProduct = new Product
            {
                Name = productDTO.Name,
                Brand = productDTO.Brand,
                Stock = productDTO.Stock,
                CategoryId = productDTO.CategoryId,
                Price = productDTO.Price,
            };

            try
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                _context.Entry(newProduct).Reload();
                return StatusCode(500);
            }
        }

        [HttpPut("/Product/{id}")]
        public ActionResult Update(int id, ProductDTOInsert productDTO)
        {
            var oldProduct = _context.Products.Where(f => f.Id == id).FirstOrDefault();
            if (oldProduct == null) return NotFound();

            oldProduct.Name = productDTO.Name;
            oldProduct.Brand = productDTO.Brand;
            oldProduct.Stock = productDTO.Stock;
            oldProduct.CategoryId = productDTO.CategoryId;
            oldProduct.Price = productDTO.Price;

            try
            {
                _context.Products.Update(oldProduct);
                _context.SaveChanges();
                return Ok(oldProduct);
            }
            catch
            {
                _context.Entry(oldProduct).Reload();
                return StatusCode(500);
            }
        }

        [HttpDelete("/Product/{id}")]
        public ActionResult Delete(int id)
        {
            var oldProduct = _context.Products.Where(f => f.Id == id).FirstOrDefault();
            if (oldProduct == null) return NotFound();

            try
            {
                _context.Products.Remove(oldProduct);
                _context.SaveChanges();
                return Ok(oldProduct);
            }
            catch
            {
                _context.Entry(oldProduct).Reload();
                return StatusCode(500);
            }
        }
    }
}
