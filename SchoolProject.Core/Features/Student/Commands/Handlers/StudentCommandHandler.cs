using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Core.Interfaces.Repositories;
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
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        #endregion

        #region Constructors
        public StudentCommandHandler(
            IMapper mapper,
            IStudentRepository studentService,
            IStringLocalizer<SharedResource> localizer,
            IUnitOfWorkAsync unitOfWorkAsync)
            : base(localizer, mapper)
        {
            _studentRepository = studentService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Data.Entities.Student>(request);

            await _studentRepository.AddAsync(student);
            await _unitOfWorkAsync.SaveChangesAsync();

            return Created<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            // Todo: Check if the student exists before updating
            // Todo: Check if the department exists before updating
            // Todo: Check if the student name already exists before updating
            var student = _mapper.Map<Data.Entities.Student>(request);

            await _studentRepository.UpdateAsync(student);
            await _unitOfWorkAsync.SaveChangesAsync();

            return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);
            if (student is null)
                return NotFound<string>(_localizer[SharedResourceKeys.NotFound]);

            await _studentRepository.DeleteAsync(student);
            await _unitOfWorkAsync.SaveChangesAsync();

            return Deleted<string>();
        }
        #endregion
    }
}