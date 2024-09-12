using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_03.DAL.Models;
using MVC_03.PLL.Interfaces;
using System;

namespace MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository iEmployeeRepository;

        private readonly IWebHostEnvironment Env;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env)
        {
            this.iEmployeeRepository = repository;
            Env = env;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employee = iEmployeeRepository.GetAll();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = iEmployeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(employee);
        }
        
        [HttpGet]
        public IActionResult Detailes(int? id, string viewName = "Detailes")
        {
            if (!id.HasValue)
            {
                return BadRequest();

            }
            var employee = iEmployeeRepository.GetById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(viewName, employee);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
           
            return Detailes(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                iEmployeeRepository.Update(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

               
                if (Env.IsDevelopment())
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Erorr Occured During Update Employee");
                }
                return View(employee);


            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Detailes(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                iEmployeeRepository.Delete(employee);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                if (Env.IsDevelopment())
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Erorr Occured During delete Employee");
                }
                return View(employee);

            }
        }
    }
}
