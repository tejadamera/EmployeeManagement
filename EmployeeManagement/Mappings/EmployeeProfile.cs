using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
namespace EmployeeManagement.Mappings
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto,Employee>();
            CreateMap<UpdateEmployeeDto,Employee>();
        }
    }
}
