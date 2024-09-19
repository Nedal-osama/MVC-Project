using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_03.DAL.Models;
using MVC_03.PL.Helpers;
using MVC_03.PL.ViewModels;
using MVC_03.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
      //  private readonly IEmployeeRepository iEmployeeRepository;
        private readonly IUniteOfWork uniteOfWork;
        private readonly IWebHostEnvironment Env;
        private readonly IMapper mapper;
        //private readonly IDepartmentRepository departmentRepository;

        public EmployeeController(IUniteOfWork uniteOfWork, IWebHostEnvironment env,IMapper mapper)
        {
          //   this.iEmployeeRepository = iEmployeeRepository;
              this.uniteOfWork = uniteOfWork;
              Env = env;
              this.mapper = mapper;
          //   this.departmentRepository = departmentRepository;
        }
     //   [HttpGet]
        public IActionResult Index( string SearchIn)
        {
            if (string.IsNullOrEmpty(SearchIn))
            {

                var employee = uniteOfWork.EmployeeRepository.GetAll();
               var mappedEmp=mapper.Map<IEnumerable< Employee>, IEnumerable<EmployeeViewModel>>(employee);
                return View(mappedEmp);
            }
            else
            {
                var employee = uniteOfWork.EmployeeRepository.GetEmployeesBYName(SearchIn.ToLower());
                var mappedEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);

                return View(mappedEmp);
            }
           
          //  ViewData["Message"] = "hello ViewData";
           
        }
        [HttpGet]
        public IActionResult Create()
        {
          //  ViewData["Departments"]=departmentRepository.GetAll();
           //    ViewBag.Departments=departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(EmployeeViewModel employeeVm)
        {
            employeeVm.ImageName = await DocumentSettings.UploadFileAsync(employeeVm.Image, "Images");
            var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVm);
             uniteOfWork.EmployeeRepository.Add(mappedEmp);
                 var count=  uniteOfWork.complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
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
            var employee = uniteOfWork.EmployeeRepository.GetById(id.Value);
            var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);
            //   ViewBag.Departments = departmentRepository.GetAll(); 
            if (employee == null)
            {
                return NotFound();
            }
           
            return View(viewName, mappedEmp);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
           
            return Detailes(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync([FromRoute] int? id, EmployeeViewModel employeeVm)
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
                employeeVm.ImageName = await DocumentSettings.UploadFileAsync(employeeVm.Image, "Images");
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                uniteOfWork.EmployeeRepository.Update(mappedEmp);
                uniteOfWork.complete();
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
        public IActionResult Delete(int? id)
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
                uniteOfWork.EmployeeRepository.Delete(mappedEmp);
                uniteOfWork.complete();
                DocumentSettings.DeletFile(employeeVm.ImageName, "Images");
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
