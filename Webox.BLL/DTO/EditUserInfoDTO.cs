using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class EditUserInfoDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
