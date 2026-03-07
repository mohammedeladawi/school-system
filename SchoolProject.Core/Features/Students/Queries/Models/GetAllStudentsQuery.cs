using MediatR;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.Students.Queries.Models;

public class GetAllStudentsQuery : IRequest<List<StudentDtoForList>>
{

}