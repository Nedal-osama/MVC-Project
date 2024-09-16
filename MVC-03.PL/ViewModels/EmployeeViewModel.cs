using MVC_03.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;

namespace MVC_03.PL.ViewModels
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1

        ,
        [EnumMember(Value = "Female")]
        Female = 2
    }
    public enum Employeey
    {
        FullTime = 1,
        PartTimetable = 2,
    }
    public class EmployeeViewModel
    {
            public int Id { get; set; }

            //[Range(18, 60)]
            public int? Age { get; set; }
            //   [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}$",ErrorMessage ="Address Must be like 123-streate")]
            public string Address { get; set; }

            //[Required(ErrorMessage = "Name Is Required !")]
            public string Name { get; set; }


            public decimal Salary { get; set; }

            public bool IsActived { get; set; }

            //[EmailAddress]
            public string Email { get; set; }
            //[Phone]
          //  [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

        //    [Display(Name = "Hire Date")]
            public DateTime HireDate { get; set; }
           
            public Gender gender { get; set; }
            //   [InverseProperty(nameof(Models.Department.Employees))]
            public Department Department { get; set; }
            public int? DepartmentId { get; set; } //FK colomn
        }
}
