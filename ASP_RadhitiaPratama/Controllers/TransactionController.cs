using ASP_RadhitiaPratama.DTOS;
using ASP_RadhitiaPratama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Net.Security;

namespace ASP_RadhitiaPratama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(EsemkaStoreContext context) : ControllerBase
    {
        private readonly EsemkaStoreContext _context = context;

        [HttpGet("/Transaction")]
        public ActionResult All([FromQuery] DateTime? minDate, [FromQuery] DateTime? maxDate)
        {
            var query = _context.Transactions.AsQueryable();

            if (minDate != null && maxDate != null)
            {
                minDate = DateTime.Parse(minDate?.ToString("yyyy-MM-dd"));
                maxDate = DateTime.Parse(maxDate?.ToString("yyyy-MM-dd"));

                query = query.Where(f => f.TransactionDate >= minDate && f.TransactionDate <= maxDate);
            }
            return Ok(query.ToList());
        }

        [HttpGet("/Transaction/{id}")]
        public ActionResult GetByID(int id)
        {
            var query = _context.Transactions.Where(f => f.Id == id).FirstOrDefault();

            if (query == null) return NotFound();

            var orders = _context.Orders.Where(f => f.TransactionId == query.Id).ToList();

            var transaction = new
            {
                id = query.Id,
                CustomerName = query.CustomerName,
                TransactionDate = query.TransactionDate,
                Orders = orders.Select(g => new
                {
                    produtName = _context.Products.Where(f => f.Id == g.ProductId).First().Name,
                    Qty = g.Qty
                }),
            };

            return Ok(transaction);
        }

        [HttpPost("/Transaction")]
        public ActionResult Create([FromBody] TransactionDTOInsert transactionDTO)
        {
            var transaction = new Transaction
            {
                CustomerName = transactionDTO.CustomerName,
                TransactionDate = transactionDTO.TransactionDate,
            };

            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                var transactionID = transaction.Id;

                try
                {
                    _context.Orders.AddRange(transactionDTO.orders.Select(f => new Order
                    {
                        ProductId = f.ProductId,
                        Qty = f.Qty,
                        TransactionId = transactionID,
                    }));

                    _context.SaveChanges();

                    return Ok(transactionDTO);
                }
                catch
                {
                    _context.Entry(transaction).State = EntityState.Unchanged;
                    _context.Entry(transactionDTO.orders.Select(f => new Order
                    {
                        ProductId = f.ProductId,
                        Qty = f.Qty,
                        TransactionId = transactionID,
                    })).State = EntityState.Unchanged;

                    return StatusCode(500);
                }

            }
            catch
            {
                _context.Entry(transaction).State = EntityState.Unchanged;
                return StatusCode(500);

            }
        }
    }
}
