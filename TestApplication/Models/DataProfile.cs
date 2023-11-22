using AutoMapper;
using TestApplication.Models.DataModels;
using TestApplication.Models.DtoModels;
using TestApplication.Models.QueryModels;

namespace TestApplication.Models
{
    public class DataProfile: Profile
    {
        public DataProfile() {

            CreateMap<Employee, EmployeeDto>()
                 .ReverseMap()
                 .ForMember(dest => dest.Companies, opt => opt.Ignore());

            CreateMap<Company, CompanyDto>()
                 .ReverseMap()
                 .ForMember(dest => dest.Employees, opt => opt.Ignore());

            CreateMap<EmployeeQuery, Employee>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Enum.Parse<TitleEnum>(src.Title)))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.Companies, opt => opt.MapFrom(src => src.CompanyIds.Select(id => new Company { Id = id })));

            CreateMap<EmployeeQuery, EmployeeDto>()
                     .ForMember(dest => dest.Title, opt => opt.MapFrom(src => Enum.Parse(typeof(TitleEnum), src.Title, true)));

            CreateMap<CompanyQuery, Company>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));

            CreateMap<EmployeeRequest, Employee>();

        }
       
    }
}
