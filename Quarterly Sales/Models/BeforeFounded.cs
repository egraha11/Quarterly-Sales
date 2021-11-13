using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quarterly_Sales.Models
{
    public class BeforeFoundedAttribute : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {

            if (value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;

                if (dateToCheck >= new DateTime(1/1/1995))
                {
                    return ValidationResult.Success;
                }
            }

            string msg = base.ErrorMessage ?? $"{ ctx.DisplayName} must be before 1/1/1995";
            return new ValidationResult(msg);
        }
    }
}
