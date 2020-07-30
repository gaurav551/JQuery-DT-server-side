using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JQueryDT.Models;
using JQueryDT.Data;
using System.Linq.Dynamic.Core;
using System.Threading;

namespace JQueryDT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        TimeSpan d;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        public string name = "How Are You?";

        public IActionResult Index()
        {
        //    Thread t = new Thread(delegate(){
        //        SeedData();
        //    });
        //    t.Start();
           Task t = Task.Factory.StartNew(()=> SeedData());
         
           
       
            ViewBag.Time = name;
            return View();
        }
        public Task SeedData()
        {   if (!_context.Students.Any())
            {
                for (int i = 1; i <= 15000; i++)
                {
                    Student s = new Student();
                    s.FirstName = "Gaurav" + i;
                    s.LastName = "Acharya" + i;
                    s.Class = "One" + i;
                    s.PhoneNumber = "981491372" + i;
                    s.Section = i.ToString();
                    s.Address = "Jhapa" + i;
                    _context.Students.Add(s);
                     _context.SaveChangesAsync();
                }
            }
            return Task.CompletedTask;
           
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetStudents()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var students = (from student in _context.Students select student);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    students = students.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    students = students.Where(m => m.FirstName.Contains(searchValue)
                                                || m.LastName.Contains(searchValue)
                                                || m.Address.Contains(searchValue)
                                                || m.PhoneNumber.Contains(searchValue));
                }
                recordsTotal = students.Count();
                var data = students.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
