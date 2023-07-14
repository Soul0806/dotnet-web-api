using System;
using System.Collections.Generic;

namespace ProductApi.Models
{
    public partial class Sale
    {
        public int Id { get; set; }
        public string Area { get; set; } = null!;
        public string Service { get; set; } = null!;
        public string? Spec { get; set; }
        public string? Quantity { get; set; }
        public string Price { get; set; } = null!;
        public string Pay { get; set; } = null!;
        public string? Note { get; set; }
        public string Date { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
    }
}
