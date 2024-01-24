using AutoMapper;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.Common.Entities;

namespace Sprout.Exam.Common.Mappers.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<BaseSaveEmployeeDto, Employee>()
               .ForMember(dest => dest.TIN, opt => opt.MapFrom(src => src.Tin))
               .ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.TypeId));

            CreateMap<CreateEmployeeDto, Employee>().IncludeBase<BaseSaveEmployeeDto, Employee>();
            CreateMap<EditEmployeeDto, Employee>().IncludeBase<BaseSaveEmployeeDto, Employee>();

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Tin, opt => opt.MapFrom(src => src.TIN))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.EmployeeTypeId))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate.ToString("yyyy-MM-dd")));

            CreateMap<Employee, RegularEmployeeDto>().IncludeBase<Employee, EmployeeDto>();
            CreateMap<Employee, ContractualEmployeeDto>().IncludeBase<Employee, EmployeeDto>();
        }
    }
}
