using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using ProductApi.model;

namespace ProductApi.Controllers
{

    public class IOController : Controller
    {   
        private readonly string _savePath = @"D:\React\ui\react-app\data.json";
        private readonly JsonController _jsonCon;
        private readonly IDb _db;

        public IOController(JsonController jsonCon, IDb db) {
            _jsonCon = jsonCon;
            _db = db;
        }

        [HttpGet("/write/{num?}")]
        public async Task<Product> Write(int? num) {

            using StreamWriter writer = new StreamWriter(_savePath, false);
            // write to data.json
            string jsonString = await _jsonCon.Read(num);
            writer.Write(jsonString);
            
            // remove current DB table records

            _db.TruncateTable();

            //insert to DB
            Product product = JsonConvert.DeserializeObject<Product>(jsonString);
            List<string> list = new List<string>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Brand", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Thumbnail", typeof(string));

            //list.Add($"('%{row.Title}%', '%{row.Price}%', '%{row.Brand}%', '%{row.Category}%', '%{row.Thumbnail}%')");
            // 加入多筆資料到 DataTable
            foreach (var row in product.products)
            {
                dt.Rows.Add(row.Title, row.Price, row.Brand, row.Category, row.Thumbnail);
            }

            _db.InsertUpdateToDb(dt);

            return product;
        }
    }
}
