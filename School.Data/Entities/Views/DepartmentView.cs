using Microsoft.EntityFrameworkCore;

namespace School.Data.Entities.Views
{
    [Keyless]
    public class DepartmentView// the name must be the same as in database 
    {
        public int DID { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
}
