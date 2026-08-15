using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Instructor.Commands.AddInstructor;

public class AddInstructorHandler :
    ResponseHandler,
    IRequestHandler<AddInstructorCommand, Response<string>>
{
    #region Private Fields
    private readonly IInstructorRepository _instructorService;
    private readonly IFileService _fileService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public AddInstructorHandler(
        IMapper mapper,
        IInstructorRepository instructorService,
        IFileService fileService,
        IWebHostEnvironment webHostEnvironment,
        IStringLocalizer<SharedResource> localizer,
        IUnitOfWork unitOfWork)
        : base(localizer, mapper)
    {
        _instructorService = instructorService;
        _fileService = fileService;
        _webHostEnvironment = webHostEnvironment;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
    {
        var instructor = _mapper.Map<Data.Entities.Instructor>(request);

        var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Instructors");

        if (request.Image != null)
        {
            var imagePath = await _fileService.UploadFileAsync(request.Image, folderPath);
            instructor.ImagePath = imagePath;
        }

        await _instructorService.AddAsync(instructor);
        await _unitOfWork.SaveChangesAsync();
        return Created<string>();
    }
    #endregion
}