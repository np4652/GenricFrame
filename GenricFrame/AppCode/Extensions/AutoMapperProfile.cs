using AutoMapper;
using GenricFrame.Models;

namespace GenricFrame.AppCode.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<Student, Employee>();
        }
    }
}
