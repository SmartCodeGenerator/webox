namespace Webox.BLL.Infrastructure.QueryParams
{
    public class LaptopParams
    {
        public string ModelName { get; set; }
        public string[] Manufacturer { get; set; }
        public string[] Processor { get; set; }
        public string[] Graphics { get; set; }
        public int[] RAM { get; set; }
        public int[] SSD { get; set; }
        public float[] Screen { get; set; }
        public string[] OS { get; set; }
        public float MinWeight { get; set; }
        public float MaxWeight { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
    }
}
