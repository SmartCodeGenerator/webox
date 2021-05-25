using System;

namespace Webox.BLL.DTO
{
    public class ReviewInfoDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime PubDateTime { get; set; }
        public float Rating { get; set; }
        public string UserName { get; set; }
        public string LaptopId { get; set; }
    }
}
