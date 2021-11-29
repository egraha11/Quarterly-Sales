using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quarterly_Sales.Models;
using System.ComponentModel.DataAnnotations;

namespace Quarterly_Sales.Controllers
{
    public class HomeController : Controller
    {

        //private SalesContext context { get; set; }
        //private readonly ILogger<HomeController> _logger;

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



            /**
            var options = new QueryOptions<Sale>
            {
                Includes = "Employee",
                PageSize = builder.routes.PageSize,
                PageNumber = builder.routes.PageNumber,
                OrderByDirection = builder.routes.SortDirection
            };
            **/

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
            //ViewBag.EmployeeList = data.context.Employees.ToList();
            //ViewBag.SalesList = data.context.Sales.ToList();

            return View("Index", model);

            //return Content(model.Sales.ToString());
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


        /**
        [HttpGet]
        public IActionResult Index(int id = 0)
        {


            var sales = context.Sales.OrderBy(s => s.Year).ToList();

            ViewBag.Employees = context.Employees.OrderBy(e => e.FirstName).ToList();
           


            if(id != 0)
            {
                sales.Where(s => s.EmployeeId == id);
            }

            //calculate total sales
            double totalSales = 0;

            foreach(Sale sale in sales)
            {
                totalSales += sale.Ammount;
            }


            ViewBag.TotalSales = totalSales;
            return View(sales);
        }
        **/


        /**
        [HttpPost]
        public IActionResult Index(Employee employee)
        {

            if (employee.EmployeeId > 0)
            {
                return RedirectToAction("Index", new { id = employee.EmployeeId });
            }
            else
            {
                
                return RedirectToAction("Index", new { id = string.Empty });
            }
        }
        **/


        /**
        public ViewResult List()
        {

        }
        **/









        /**
        [HttpPost]
        public IActionResult Index(int employeeId)
        {

            var sales = context.Sales.Where(s => s.EmployeeId == employeeId).OrderBy(s => s.Year).ToList();

            ViewBag.Employees = context.Employees.OrderBy(e => e.FirstName).ToList();

            //calculate total sales
            double totalSales = 0;

            foreach (Sale sale in sales)
            {
                totalSales += sale.Ammount;
            }

            ViewBag.TotalSales = totalSales;

            return View(sales);
        }
        **/


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
