using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Api
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(destination => destination.ConfirmEmail,
                           option => option.MapFrom(source => source.Email));

            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
