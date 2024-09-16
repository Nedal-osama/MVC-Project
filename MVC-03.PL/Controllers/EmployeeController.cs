using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_03.DAL.Models;
using MVC_03.PL.ViewModels;
using MVC_03.PLL.Interfaces;
using MVC_03.PLL.Repositries;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository iEmployeeRepository;

        private readonly IWebHostEnvironment Env;
        private readonly IMapper mapper;
        private readonly IDepartmentRepository departmentRepository;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env,IMapper mapper/*,IDepartmentRepository departmentRepository*/)
        {
            this.iEmployeeRepository = repository;
            Env = env;
            this.mapper = mapper;
            // this.departmentRepository = departmentRepository;
        }
     //   [HttpGet]
        public IActionResult Index( string SearchIn)
        {
            if (string.IsNullOrEmpty(SearchIn))
            {

                var employee = iEmployeeRepository.GetAll();
               var mappedEmp=mapper.Map<IEnumerable< Employee>, IEnumerable<EmployeeViewModel>>(employee);
                return View(mappedEmp);
            }
            else
            {
                var employee = iEmployeeRepository.GetEmployeesBYName(SearchIn.ToLower());
                var mappedEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);

                return View(mappedEmp);
            }
           
          //  ViewData["Message"] = "hello ViewData";
           
        }
        [HttpGet]
        public IActionResult Create()
        {
         //   ViewData["Departments"]=departmentRepository.GetAll();
           //    ViewBag.Departments=departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid)
            {
                var mappedEmp=mapper.Map<EmployeeViewModel,Employee>(employeeVm);
                var count = iEmployeeRepository.Add(mappedEmp);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(employeeVm);
        }
        
        [HttpGet]
        public IActionResult Detailes(int? id, string viewName = "Detailes")
        {
            if (!id.HasValue)
            {
                return BadRequest();

            }
            var employee = iEmployeeRepository.GetById(id.Value);
            ViewBag.Departments = departmentRepository.GetAll(); 
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employeeVm);
            }

            try
            {
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                iEmployeeRepository.Update(mappedEmp);
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
                return View(employeeVm);


            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Detailes(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel employeeVm)
        {
            try
            {
              var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                iEmployeeRepository.Delete(mappedEmp);
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
                return View(employeeVm);

            }
        }
    }
}
