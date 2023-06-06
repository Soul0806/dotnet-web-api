using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ProductApi.model;

namespace ProductApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id?}")]
        public JsonResult Get(int? id = null)
        {
            string query;
            if (id.HasValue)
            {
                query = @"
                            select * from
                            dbo.Product where ID = (@id)                        
                            ";
            }
            else
            {
                query = @"
                            select * from
                            dbo.Product          
                            ";
            }

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnetion");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    if (id.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@id", id);
                    }
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
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
            return new JsonResult(table);
        }
    }
}
