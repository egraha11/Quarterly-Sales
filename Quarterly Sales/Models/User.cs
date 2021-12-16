using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quarterly_Sales.Models
{
    public class User : IdentityUser
    {

        
        [NotMapped]
        public IList<string> RoleNames { get; set; }

    }
}
