﻿using Microsoft.AspNetCore.Mvc;
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
            List<Merchandise> merchan;
            if (id.HasValue) {
                merchan = _context.Merchandise.Where(m => m.ID == id).ToList();
            }
            else {
                merchan = _context.Merchandise.ToList();
            }

            return new JsonResult(merchan);
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

            return new JsonResult(m);
        }

        [HttpPost]
        public async Task<JsonResult> Post(Merchandise merchan)
        {
            _context.Merchandise.Add(merchan);
            await _context.SaveChangesAsync();
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public async Task<JsonResult> Put(Merchandise merchan)
        {
            var entity = _context.Merchandise.FirstOrDefault(m => m.ID == merchan.ID);
            if (entity != null)
            {
                entity.Title = merchan.Title;
                entity.Price = merchan.Price;
                entity.Brand = merchan.Brand;
                entity.Category = merchan.Category;
                entity.Thumbnail = merchan.Thumbnail;

                await _context.SaveChangesAsync();
                return new JsonResult("Updated Successfully");
            }
            return new JsonResult("not found");
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            var entity = _context.Merchandise.FirstOrDefault(e => e.ID == id);
            if (entity != null)
            {
                _context.Merchandise.Remove(entity);
                await _context.SaveChangesAsync();
                return new JsonResult("Deleted Successfully");
            }
            return new JsonResult("not found");
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
