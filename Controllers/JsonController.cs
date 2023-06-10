using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Xml.Linq;

namespace ProductApi.Controllers
{
    public class JsonController : Controller
    {
        private readonly string _productApi = "https://dummyjson.com/products";
        public JsonController() { 
        }


        public async Task<string> Read(int? num)
        {
           
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync($"{_productApi}?limit={num}");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return json;
            }
            else
            {
                throw new Exception("Failed to retrieve JSON data");
            }
        }
    }
}
