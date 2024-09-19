using MVC_03.DAL.Data;
using MVC_03.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.PLL.Repositries
{
    public class uniteOfWork : IUniteOfWork 
    {
        public uniteOfWork(AppDbContext dbContext)
        {
            DbContext = dbContext;
            EmployeeRepository = new EmplyeeRepository(DbContext);
            DepartmentRepository = new DepartmentRepository(DbContext);
           
        }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }
        public AppDbContext DbContext { get; }

        public int complete()
        {
            return DbContext.SaveChanges();
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
