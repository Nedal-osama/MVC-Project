using AutoMapper;
using MVC_03.DAL.Models;
using MVC_03.PL.ViewModels;

namespace MVC_03.PL.Helpers
{
    public class mappingProfile:Profile
    {
        public mappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
