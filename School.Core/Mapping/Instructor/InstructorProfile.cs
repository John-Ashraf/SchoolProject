using AutoMapper;

namespace School.Core.Mapping.Instructor
{
    public partial class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            AddInstructorMapping();

        }
    }
}
