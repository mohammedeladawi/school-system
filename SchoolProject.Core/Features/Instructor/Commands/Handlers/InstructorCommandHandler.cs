using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructor.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Instructor.Commands.Handlers;

public class InstructorCommandHandler :
    ResponseHandler,
    IRequestHandler<AddInstructorCommand, Response<string>>

{
    #region Private Fields
    private readonly IInstructorService _instructorService;
    private readonly IFileService _fileService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    #endregion

    #region Constructors
    public InstructorCommandHandler(
        IMapper mapper,
        IInstructorService instructorService,
        IFileService fileService,
        IWebHostEnvironment webHostEnvironment,
        IStringLocalizer<SharedResource> localizer)
        : base(localizer, mapper)
    {
        _instructorService = instructorService;
        _fileService = fileService;
        _webHostEnvironment = webHostEnvironment;
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
        return Created<string>();
    }
    #endregion
}