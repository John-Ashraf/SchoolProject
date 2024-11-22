using School.Core.Features.Departments.Queries.Response;
using School.Data.Entities;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            _ = CreateMap<Department, GetDepartmentByIdResponse>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.DName))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(x => x.ManagerName, opt => opt.MapFrom(src => src.Instructor.EName))
                .ForMember(x => x.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                //.ForMember(x => x.StudentList, opt => opt.MapFrom(src => src.Students))
                .ForMember(x => x.InstructorList, opt => opt.MapFrom(src => src.Instructors));
            _ = CreateMap<DepartmetSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.SubjectNameEn))
                ;
            //_ = CreateMap<Student, StudentResponse>()
            //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            _ = CreateMap<Instructor, InstructorResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EName));

        }
    }
}
