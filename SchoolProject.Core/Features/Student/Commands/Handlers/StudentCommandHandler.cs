using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Commands.Handlers
{
    public class StudentCommandHandler :
        ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentByIdCommand, Response<string>>

    {
        #region Private Fields
        private readonly IStudentService _studentService;
        #endregion

        #region Constructors
        public StudentCommandHandler(
            IMapper mapper,
            IStudentService studentService,
            IStringLocalizer<SharedResource> localizer)
            : base(localizer, mapper)
        {
            _studentService = studentService;
        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Data.Entities.Student>(request);
            await _studentService.AddAsync(student);
            return Created<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Data.Entities.Student>(request);
            await _studentService.UpdateAsync(student);
            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(request.Id);
            if (student is null)
                return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);
            await _studentService.DeleteAsync(student);
            return Deleted<string>();
        }
        #endregion
    }
}