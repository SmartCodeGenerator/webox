using System.Collections.Generic;

namespace Webox.DAL.Entities
{
    public class Laptop
    {
        public string LaptopId { get; set; }
        public string ModelName { get; set; }
        public string Manufacturer { get; set; }
        public string Processor { get; set; }
        public string GraphicsCard { get; set; }
        public int RAMCapacity { get; set; }
        public int SSDCapacity { get; set; }
        public float ScreenSize { get; set; }
        public string OS { get; set; }
        public float Weight { get; set; }
        public float Price { get; set; }
        public float Rating { get; set; }
        public bool IsAvailable { get; set; }
        public string ModelImagePath { get; set; }

        public ICollection<StorageLot> StorageLots { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Preference> Preferences { get; set; }
        public ICollection<Comparison> Comparisons { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
