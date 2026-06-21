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
        IRequestHandler<DeleteStudentByIdCommand, Response<string>>

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
            await _studentService.AddAsync(student);
            return Created<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            await _studentService.UpdateAsync(student);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
        {
            await _studentService.DeleteByIdAsync(request.Id);
            return Deleted<string>();
        }
    }
}