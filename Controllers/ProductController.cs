using Microsoft.AspNetCore.Mvc;
using System.Data;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using ProductApi.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProductApi.Data;

namespace ProductApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _context;

        public ProductController(IConfiguration configuration, MyDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("{id?}")]
        public JsonResult Get(int? id = null)
        {
            List<Merchandise> m;
            if (id.HasValue)
            {
               m = _context.Merchandise.Where(m => m.ID == id).ToList();
                            
            }
            else
            {
              m = _context.Merchandise.ToList();
            }

            //DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("DefaultConnetion");
            //SqlDataReader myReader;
            //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            //{
            //    myCon.Open();
            //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            //    {
            //        if (id.HasValue)
            //        {
            //            myCommand.Parameters.AddWithValue("@id", id);
            //        }
            //        myReader = myCommand.ExecuteReader();
            //        table.Load(myReader);
            //        myReader.Close();
            //        myCon.Close();
            //    }
            //}
            return new JsonResult(m);
        }


        [HttpGet ("page/{page}")]
        public JsonResult Get(int page)
        {
            if (page == 0 || page == null) {
                page = 1;
            }
            
            const int Limit = 15;
            int offset = (Convert.ToInt32(page) - 1) * Limit;
            List<Merchandise> m = _context.Merchandise.Skip(offset).Take(Limit).ToList();

            //string query = @"
            //                SELECT * FROM  dbo.Product     
            //                ORDER BY ID
            //                OFFSET @offset ROWS
            //                FETCH NEXT @Limit ROWS ONLY;
            //             ";

            //DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("DefaultConnetion");
            //SqlDataReader myReader;
            //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            //{
            //    myCon.Open();
            //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            //    {
            //         myCommand.Parameters.AddWithValue("@offset", offset);
            //         myCommand.Parameters.AddWithValue("@Limit", Limit);
                 
            //        myReader = myCommand.ExecuteReader();
            //        table.Load(myReader);
            //        myReader.Close();
            //        myCon.Close();
            //    }
            //}


            return new JsonResult(m);
        }

        [HttpPost]
        public JsonResult Post(Product p)
        {
            string query = @"
                           insert into dbo.Product (Title, Price, Brand, Category, Thumbnail)
                           values (@Title, @Price, @Brand, @Category, @Thumbnail)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnetion");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Title", p.Title);
                    myCommand.Parameters.AddWithValue("@Price", p.Price);
                    myCommand.Parameters.AddWithValue("@Brand", p.Brand);
                    myCommand.Parameters.AddWithValue("@Category", p.Category);
                    myCommand.Parameters.AddWithValue("@Thumbnail", p.Thumbnail);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Product p)
        {
            string query = @"
                           update dbo.Product
                           set       
                           Title = (@Title),
                           Price = (@Price),
                           Brand = (@Brand),
                           Category = (@Category)
                           where ID = (@id)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnetion");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Title", p.Title);
                    myCommand.Parameters.AddWithValue("@Price", p.Price);
                    myCommand.Parameters.AddWithValue("@Brand", p.Brand);
                    myCommand.Parameters.AddWithValue("@Category", p.Category);
                    myCommand.Parameters.AddWithValue("@id", p.ID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Product
                           where ID = (@id)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnetion");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


        [HttpGet("Category")]
        public JsonResult Category(int id)
        {
            List<Merchandise> m = _context.Merchandise.ToList();
            string query = @"
                           select distinct Category
                           from dbo.Product
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnetion");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(m);
        }

        //[HttpGet("test")]
        //public JsonResult Test() {
        //    List<Merchandise> m = _context.Merchandise.ToList();
        //    return new JsonResult(m);
        //}
    }
}
