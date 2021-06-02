using System;
using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class StorageReplenishmentDTO
    {

        [Required, Range(1, 100)]
        public int LaptopsAmount { get; set; }
    }
}
