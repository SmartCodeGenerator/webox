namespace Webox.BLL.DTO
{
    public class LaptopWithIdDTO : LaptopDTO
    {
        public string Id { get; set; }
        public ReviewInfoDTO[] Reviews { get; set; }
        public StorageLotInfoDTO[] StorageLots { get; set; }
    }
}
