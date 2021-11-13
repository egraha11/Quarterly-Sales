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

        private SalesContext context { get; set; }
        //private readonly ILogger<HomeController> _logger;

        public HomeController(SalesContext ctx)
        {
            context = ctx;
        }


        /**
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        **/



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


        [HttpPost]
        public RedirectToActionResult Index(Employee employee)
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
