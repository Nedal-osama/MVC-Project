using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.PLL.Interfaces
{
    public interface IUniteOfWork: IDisposable
    {
        public IEmployeeRepository EmployeeRepository{ get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        int complete();
    }
}
