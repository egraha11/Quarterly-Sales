using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quarterly_Sales.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Please Enter A UserName")]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter A Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm Your Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
