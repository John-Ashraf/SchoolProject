using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MediatR;
using School.Data.Entities;
using School.Core.Features.Students.Response;
using School.Core.Bases;
namespace School.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentListResponse>>>
    {


    }
}
