using AutoMapper;
using EmployeeManagement.Api.Dtos;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
