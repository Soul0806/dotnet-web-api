using System;
using System.Collections.Generic;

namespace ProductApi.Models
{
    public partial class Merchandise
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
    }
}
