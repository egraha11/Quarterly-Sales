using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quarterly_Sales.Models
{
    public class After2000Attribute : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {

            if (value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;

                if (dateToCheck.Year > 2000)
                {
                    return ValidationResult.Success;
                }
            }

            string msg = base.ErrorMessage ?? $"{ ctx.DisplayName} must be a year after 2000";
            return new ValidationResult(msg);
        }
    }
}