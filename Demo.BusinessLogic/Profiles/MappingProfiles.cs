using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models.DepartmentModel;
using DataAccess.Models.EmployeeModel;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;
using Microsoft.Extensions.Options;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<UpdateEmployeeDto, Employee>().
                ForMember(dest => dest.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
            CreateMap<Employee, EmployeeDto>().
                ForMember(E => E.EmpGender, Options => Options.MapFrom(src => src.Gender)).
                ForMember(E => E.EmpType, Options => Options.MapFrom(src => src.EmployeeType)).
                ForMember(E => E.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));
            CreateMap<Employee, EmployeeDetailsDto>().
                ForMember(E => E.Gender, Options => Options.MapFrom(src => src.Gender)).
                ForMember(E => E.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType)).
                ForMember(E=>E.HiringDate,Options=>Options.MapFrom(src=>DateOnly.FromDateTime(src.HiringDate))).
                ForMember(E => E.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<CreatedEmplopyeeDto, Employee>()
                .ForMember(dest=>dest.HiringDate,Options=>Options.MapFrom(src=>src.HiringDate.ToDateTime(TimeOnly.MinValue))).ReverseMap();
        }
    }
}
