using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.EmployeeModel;

namespace DataAccess.Data.Configurations
{
    public class EmployeeConfigurations :BaseEntityConfigurations<Employee> ,IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e=>e.Name).HasColumnType("nvarchar(50)");
            builder.Property(e=>e.Address).HasColumnType("nvarchar(50)");
            builder.Property(e=>e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(e=>e.Gender).HasConversion((EmpGender)=> EmpGender.ToString(),
                (_gender) =>(Gender)Enum.Parse(typeof(Gender),_gender));
            builder.Property(e => e.EmployeeType).HasConversion((EmpType) => EmpType.ToString(),
                (_Type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), _Type));
            base.Configure(builder);


        }
    }
}
