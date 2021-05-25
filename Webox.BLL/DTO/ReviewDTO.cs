using System;
using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class ReviewDTO
    {
        public string ReviewText { get; set; }
        [Required, Range(0, 5)]
        public float Rating { get; set; }
        [Required]
        public string LaptopId { get; set; }
    }
}
