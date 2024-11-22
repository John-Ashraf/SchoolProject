namespace School.Core.Features.Students.Response
{
    public class GetStudentPaginatedListResponse
    {
        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? Phone { get; set; }
        public string? Departmentname { get; set; }
        public GetStudentPaginatedListResponse(int studid, string name, string address, string phone, string department)
        {
            StudID = studid;
            Name = name;
            Address = address;
            Phone = phone;
            Departmentname = department;

        }
    }
}
