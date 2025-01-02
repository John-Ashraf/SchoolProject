using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.infrastructure.Abstracts;
using School.infrastructure.Data;
using School.Services.Abstracts;

namespace School.Services.Implementation
{
    public class InstructorService : IInstructorService
    {
        #region Fields
        private readonly ApplicationDbContext _dbContext;
        private readonly IInstructorInf _instructorsRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public InstructorService(ApplicationDbContext dpcontext,
            IInstructorInf instructorInf, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dpcontext;
            _fileService = fileService;
            _instructorsRepository = instructorInf;
            _httpContextAccessor = httpContextAccessor;

        }

        #endregion
        #region HandleFunctions

        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage("Instructors", file);
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            instructor.Image = baseUrl + imageUrl;
            try
            {
                _ = await _instructorsRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception ex)
            {
                return "FailedInAdd+ " + ex.Message;
            }
        }

        public async Task<bool> IsNameExist(string name)
        {
            var instructor = _instructorsRepository.GetTableNoTracking().Any(x => x.EName == name);
            if (instructor) return true;
            return false;
        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            var instructor = _instructorsRepository.GetTableNoTracking().Where(x => x.InsId != id && x.EName == name).FirstOrDefaultAsync();
            if (instructor != null) return true;
            return false;
        }
        #endregion
    }
}
