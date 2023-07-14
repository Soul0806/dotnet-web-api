using System;
using System.Collections.Generic;

namespace ProductApi.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Price { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? Thumbnail { get; set; }
    }
}
