using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Quarterly_Sales.Models
{
    public class Sale
    {

        public int SaleId { get; set; }

        [Required]
        [Range(1, 4)]
        public int Quarter { get; set; }

        [Required]
        [After2000]
        public DateTime Year { get; set; }

        [Required]
        [GreaterThanZero]
        public double Ammount { get; set; }


        [Required]
        [Remote("CheckSale", "Validation", AdditionalFields ="Quarter, Year")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
