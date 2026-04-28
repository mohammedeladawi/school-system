using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler :
        ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>

    {

        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public StudentCommandHandler(IMapper mapper, IStudentService studentService,
             IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            bool result = await _studentService.AddStudentAsync(student);

            if (result)
                return Created<string>("Student added successfully.");
            else
                return UnprocessableEntity<string>("Failed to add student.");
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            bool result = await _studentService.EditStudentAsync(student);

            if (result)
                return Success<string>("Student updated successfully.");
            else
                return UnprocessableEntity<string>("Failed to update student.");
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            bool isDeleted = await _studentService.DeleteByIdAsync(request.Id);

            if (isDeleted)
                return Deleted<string>("Student deleted successfully.");
            else
                return NotFound<string>("Failed to delete student.");

        }
    }
}