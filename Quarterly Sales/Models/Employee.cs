using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Quarterly_Sales.Models
{
    public class Employee
    {


        
        public int EmployeeId { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        [PastDate]
        [Remote("CheckEmployee", "Validation", AdditionalFields = "FirstName, LastName")]
        public DateTime DOB { get; set; }

        [Required]
        [PastDate]
        [BeforeFounded]
        public DateTime DateOfHire { get; set; }

        [Required]
        [Remote("CheckManager", "Validation", AdditionalFields = "FirstName, LastName, DOB")]
        public int ManagerId { get; set; }
    }
}
