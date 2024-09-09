using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVC_03.DAL.Models.Employee;

namespace MVC_03.DAL.Data.Configrations
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).IsRequired();
            builder.Property(E=>E.Salary).HasColumnType("integer");
            builder.Property(E => E.gender).HasConversion(
                (gender) => gender.ToString(),
                (genderAsString)=>(Gender)Enum.Parse(typeof(Gender), genderAsString,true)
                
                );
        }
    }
}
