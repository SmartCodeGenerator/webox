using Webox.DAL.Entities;

namespace Webox.BLL.DTO
{
    public class LaptopWithIdDTO : LaptopDTO
    {
        public string Id { get; set; }
        public ReviewInfoDTO[] Reviews { get; set; }
    }
}
