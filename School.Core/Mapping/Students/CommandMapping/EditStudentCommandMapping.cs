using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                .ForMember(Es => Es.StudID, opt => opt.MapFrom(src => src.Id))
                .ForMember(Es => Es.DID, opt => opt.MapFrom(src => src.DepartmentId));



        }
    }
}
