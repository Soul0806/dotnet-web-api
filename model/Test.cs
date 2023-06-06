using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.model
{

    public class Test
    {
        public int testID { get; set; }

        [JsonProperty("title")]
        public string first { get; set; }

        [JsonProperty("price")]
        public string last { get; set; }

    }
}
