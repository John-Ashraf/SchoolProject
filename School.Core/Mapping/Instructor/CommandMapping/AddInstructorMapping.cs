
using School.Core.Features.Instructors.Commands.Models;

namespace School.Core.Mapping.Instructor
{
    public partial class InstructorProfile
    {
        void AddInstructorMapping()
        {
            _ = CreateMap<AddInstructorCommand, Data.Entities.Instructor>()
                .ForMember(dest => dest.EName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.SupervisorId, opt => opt.MapFrom(src => src.SupervisorId))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DID));
        }
    }
}
