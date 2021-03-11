using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class LoginDTO
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
