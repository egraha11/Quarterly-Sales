using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quarterly_Sales.Models
{
    public class PastDateAttribute : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {

            if(value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;

                if(dateToCheck < DateTime.Today)
                {
                    return ValidationResult.Success;
                }
            }

            string msg = base.ErrorMessage ?? $"{ ctx.DisplayName} must be a valid past date";
            return new ValidationResult(msg);
        }
    }
}
