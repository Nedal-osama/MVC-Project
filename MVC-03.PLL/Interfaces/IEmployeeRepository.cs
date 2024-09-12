using MVC_03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.PLL.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        //Employee
        IQueryable<Employee> GetEmployeeByAddress(string address);

    }
}
