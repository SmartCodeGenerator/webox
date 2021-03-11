using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MinLength(6), DataType(DataType.Password), Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsEmployee { get; set; }
        public string ProfileImagePath { get; set; }
    }
}
