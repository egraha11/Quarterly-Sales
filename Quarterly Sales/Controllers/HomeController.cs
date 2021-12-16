using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quarterly_Sales.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Quarterly_Sales.Controllers
{
    public class HomeController : Controller
    {

        private Repository<Sale> data;

        public HomeController(SalesContext ctx)
        {
            data = new Repository<Sale>(ctx);
        }


       


        
        [HttpGet]
        public RedirectToActionResult Index()
        {
            return RedirectToAction("List");
        }

        
        [AllowAnonymous]
        public IActionResult List(GridDTO values)
        {

            GridBuilder builder = new GridBuilder(values, HttpContext.Session);

            SortOptions sortOptions = new SortOptions
            {
                Includes = "Employee",
                PageSize = builder.routes.PageSize,
                PageNumber = builder.routes.PageNumber,
                OrderByDirection = builder.routes.SortDirection
            };


            sortOptions.SortFilters(builder);
            

            if(builder.routes.SortField == "LastName")
            {
                sortOptions.OrderBy = s => s.Employee.LastName;
            }
            else if(builder.routes.SortField == "Year")
            {
                sortOptions.OrderBy = s => s.Year;
            }
            else if (builder.routes.SortField == "Quarter")
            {
                sortOptions.OrderBy = s => s.Quarter;
            }
            else
            {
                sortOptions.OrderBy = s => s.Year;
            }


            SaleViewModel model = new SaleViewModel
            {
                Sales = data.BuildQuery(sortOptions),
                Employees = data.context.Employees.ToList().OrderBy(e => e.LastName),
                CurrentRoute = builder.routes,
                TotalPages = builder.GetTotalPages(data.Count)  
            };

            var viewSales = data.context.Sales.OrderBy(s => s.Year).ToList();

            double totalSales = 0;

            foreach (Sale sale in viewSales)
            {
                totalSales += sale.Ammount;
            }


            ViewBag.TotalSales = totalSales;


            return View("Index", model);
        }
        

        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            GridBuilder builder = new GridBuilder(HttpContext.Session);

            if(clear == true)
            {
                builder.routes.ClearFilters();
            }
            else
            {
                builder.routes.Employee = filter[0];
                builder.routes.Year = filter[1];
                builder.routes.Quarter = filter[2];
            }

            builder.Save();

            return RedirectToAction("List", builder.routes);


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
