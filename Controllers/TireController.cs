using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.model;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TireController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _context;

        public TireController(IConfiguration configuration, MyDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("{id?}")]
        public JsonResult Get(int? id = null)
        {
            List<Tire> tire;
            if (id.HasValue)
            {
                tire = _context.Tire.Where(m => m.ID == id).ToList();
            }
            else
            {
                tire = _context.Tire.ToList();
            }

            return new JsonResult(tire);
        }

        [HttpPost]
        public async Task<JsonResult> Post(Tire tire)
        {
            _context.Tire.Add(tire);
            await _context.SaveChangesAsync();
            return new JsonResult("Added Successfully");
        }

    }
}
