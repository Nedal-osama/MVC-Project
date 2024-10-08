﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using MVC_03.DAL.Models;
using MVC_03.PLL.Interfaces;
using MVC_03.PLL.Repositries;
using System;

namespace MVC_03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUniteOfWork uniteOfWork;

        //  private readonly IDepartmentRepository departmentRepository;

        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUniteOfWork uniteOfWork,IWebHostEnvironment env)
        {
            this.uniteOfWork = uniteOfWork;
            //  this.departmentRepository= departmentRepository;
            _env = env;
        }
        [HttpGet]
        public IActionResult Index()
        {
           var departments=uniteOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                  uniteOfWork.DepartmentRepository.Add(department);
                var count = uniteOfWork.complete();
                if(count>0)
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(department);
        }
        //Department/Detailes/id
        [HttpGet]
        public IActionResult Detailes(int? id,string viewName="Detailes")
        { 
          if(!id.HasValue)
            {
                return BadRequest();
            
            }
            var department= uniteOfWork.DepartmentRepository.GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(viewName,department);
        
        }

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
        /*    if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = departmentRepository.GetById(id.Value);
            if(department == null)
                return NotFound();
            return View(department);*/
        return Detailes(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if(id!=department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            try
            {
                uniteOfWork.DepartmentRepository.Update(department);
                uniteOfWork.complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

              //1-Log Exception
              //2-Freidly Msg
              if(_env.IsDevelopment()) {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Erorr Occured During Update Employee");
                }
              return View(department);


            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Detailes(id,"Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            try
            {
                uniteOfWork.DepartmentRepository.Delete(department);
                uniteOfWork.complete();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Erorr Occured During delete Employee");
                }
                return View(department);

            }
        }
    }
}
