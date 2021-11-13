using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quarterly_Sales.Models
{
    public class GreaterThanZero : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {

            if (value is double)
            {
                double ammount = (double)value;

                if (ammount > 0)
                {
                    return ValidationResult.Success;
                }
            }

            string msg = base.ErrorMessage ?? $"{ ctx.DisplayName} must be greater than 0";
            return new ValidationResult(msg);
        }
    }
}