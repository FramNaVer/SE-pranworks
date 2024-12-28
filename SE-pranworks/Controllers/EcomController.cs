using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE_pranworks.Models;
using System.Threading.Tasks;
using static SE_pranworks.Models.Modelecom;

namespace SE_pranworks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EcomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ecom/Customers      
        [HttpGet("GetCustomers")]
        public async Task<ActionResult<IEnumerable<object>>> GetCustomers(string FirstName = null, int? CustomerId = null, string LastName = null)
        {
            var customersQuery = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(FirstName))
            {
                customersQuery = customersQuery.Where(c => c.FirstName.Contains(FirstName));
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                customersQuery = customersQuery.Where(c => c.LastName.Contains(LastName));
            }

            if (CustomerId.HasValue)
            {
                customersQuery = customersQuery.Where(c => c.CustomerId == CustomerId.Value);
            }

            return Ok(customersQuery);
        }
        [HttpPost("CreateCustomers")]
        public async Task<ActionResult<Customers>> CreateCustomers(Customers customers)
        {
            if (customers == null)
            {
                return BadRequest("No way brother");
            }

            _context.Customers.Add(customers);
            await _context.SaveChangesAsync();

            var customerResponse = new
            {             
                FirstName = customers.FirstName,
                LastName = customers.LastName,
                Email = customers.Email,
                IsActive = customers.IsActive,
            };

            return CreatedAtAction(nameof(GetCustomers), new { id = customers.CustomerId }, customerResponse);
        }
    }
}
