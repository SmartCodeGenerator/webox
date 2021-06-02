using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class ResetPasswordDTO
    {
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
