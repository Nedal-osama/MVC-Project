﻿using Microsoft.EntityFrameworkCore;
using MVC_03.DAL.Data;
using MVC_03.DAL.Models;
using MVC_03.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.PLL.Repositries
{
    public class GenericReposiory<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly AppDbContext dpContext;
        public GenericReposiory(AppDbContext appDbContext)
        {
            // dpContext = new AppDbContext();
            this.dpContext = appDbContext;
        }
        public void Add(T item)
        {
            //dpContext = new AppDbContext(); XXXX
       //     dpContext.Set<T>().Add(item);
            dpContext.Add(item);
        //    return dpContext.SaveChanges();
        }

        public void Delete(T item)
        {
            dpContext.Set<T>().Remove(item);
           // return dpContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) dpContext.Employees.Include(E => E.Department).AsNoTracking().ToList();
            }
             else
            {
                return dpContext.Set<T>().AsNoTracking().ToList();  
            }
           

        }

        public T GetById(int id)
        {
            // var department = dpContext.Departments.Where(D => D.Id == 10).FirstOrDefault();
            //var department = dpContext.Departments.Local.Where(D => D.Id == 10).FirstOrDefault();
            //if (department==null)
            //{
            //    department = dpContext.Departments.Where(D => D.Id == 10).FirstOrDefault();
            //}
            return dpContext.Set<T>().Find(id);
            // return dpContext.Departments.Find<Department>(id);
        }

        public void Update(T item)
        {
            dpContext.Set<T>().Update(item);
          //  return dpContext.SaveChanges();
        }
    }
}
