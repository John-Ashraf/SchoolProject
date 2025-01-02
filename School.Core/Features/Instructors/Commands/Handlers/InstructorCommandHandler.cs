using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Instructors.Commands.Models;
using School.Data.Entities;
using School.Services.Abstracts;

namespace School.Core.Features.Instructors.Commands.Handlers
{
    public class InstructorCommandHandler : ResponseHandler, IRequestHandler<AddInstructorCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IInstructorService _instructorService;
        #endregion
        #region Constructor
        public InstructorCommandHandler(IMapper mapper, IInstructorService instructorService)
        {
            _mapper = mapper;
            _instructorService = instructorService;
        }


        #endregion
        #region HandleFunctions
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var response = await _instructorService.AddInstructorAsync(instructor, request.Image);
            switch (response)
            {
                case "NoImage": return BadRequest<string>(response);
                case "FailedToUploadImage": return BadRequest<string>(response);
                case "FailedInAdd": return BadRequest<string>(response);
            }
            return Success("");
        }
        #endregion
    }
}
