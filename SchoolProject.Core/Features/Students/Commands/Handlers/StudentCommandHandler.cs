using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler :
        ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public StudentCommandHandler(IMapper mapper, IStudentService studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            bool result = await _studentService.AddStudentAsync(student);

            if (result)
                return Success<string>("Student added successfully.");
            else
                return UnprocessableEntity<string>("Failed to add student.");
        }
    }
}