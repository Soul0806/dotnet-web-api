using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.model;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _context;

        public SaleController(IConfiguration configuration, MyDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("{id?}")]
        public JsonResult Get(int? id = null)
        {
            List<Sale> sale;
            if (id.HasValue)
            {
                sale = _context.Sale.Where(m => m.Id == id).ToList();
            }
            else
            {
                sale = _context.Sale.ToList();
            }
            return new JsonResult(sale);
        }

        [HttpGet]
        public JsonResult Get([FromQuery] string d)
        {
            List<Sale> sale;
            sale = _context.Sale.Where(s => s.Date == d).ToList();
            return new JsonResult(sale);
        }


        [HttpPost]
        public async Task<JsonResult> Post(Sale sale)
        {
            _context.Sale.Add(sale);
            await _context.SaveChangesAsync();
            return new JsonResult("Added Successfully");
        }



    }
}
