using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class DelivererDTO
    {
        [Required]
        public string CompanyName { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string MainOfficeAddress { get; set; }
    }
}
