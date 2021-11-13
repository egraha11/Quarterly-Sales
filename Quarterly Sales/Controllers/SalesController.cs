using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quarterly_Sales.Models;

namespace Quarterly_Sales.Controllers
{
    public class SalesController : Controller
    {

        SalesContext context;

        public IActionResult Index()
        {
            return View();
        }

        public SalesController(SalesContext ctx)
        {
            context = ctx;
        }
        
        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.Employees = context.Employees.OrderBy(e => e.FirstName).ToList();

            return View("Add", new Sale());
        }


        public IActionResult Add(Sale sale)
        {

            if (ModelState.IsValid)
            {
                context.Sales.Add(sale);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Employees = context.Employees.OrderBy(e => e.FirstName).ToList();

                return View("Add", sale);
            }
        }

    }
}
