using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class LaptopDTO
    {
        [Required]
        public string ModelName { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Processor { get; set; }
        [Required]
        public string Graphic { get; set; }
        [Required, Range(0, 128)]
        public int Ram { get; set; }
        [Required, Range(0, 4096)]
        public int Ssd { get; set; }
        [Required, Range(9, 17)]
        public float Screen { get; set; }
        [Required]
        public string Os { get; set; }
        [Required, Range(0, 7)]
        public float Weight { get; set; }
        [Required, Range(0, 500000)]
        public float Price { get; set; }
        [Required, Range(0, 5)]
        public float Rating { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        public string ModelImagePath { get; set; }
    }
}
