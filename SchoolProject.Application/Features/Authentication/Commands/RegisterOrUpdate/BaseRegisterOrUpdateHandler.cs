using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate;

public abstract class BaseRegisterOrUpdateUserHandler<TCommand, TUser> :
    ResponseHandler,
    IRequestHandler<TCommand, Response<string>>
    where TCommand : CommonUserCommand, IRequest<Response<string>>
    where TUser : Domain.Entities.Identities.ApplicationUser
{
    #region Protected Fields

    protected readonly IUserManager _userManager;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IFileService _fileService;
    protected readonly IUrlService _urlService;
    protected readonly IEmailService _emailService;
    protected readonly ILocationService _locationService;

    #endregion

    #region Constructors

    protected BaseRegisterOrUpdateUserHandler(
        IUserManager userManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer,
        IUrlService urlService,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        IFileService fileService,
        ILocationService locationService)
        : base(localizer, mapper)
    {
        _userManager = userManager;
        _urlService = urlService;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _locationService = locationService;
    }

    #endregion

    #region Private Methods

    private bool IsEmailChanged(TUser user, TCommand request)
    {
        return request.Email != user.Email;
    }

    private async Task UpdateImageAsync(TUser user, TCommand request, string role)
    {
        if (request.Image is null)
            return;

        RemoveOldImageIfExists(user);
        user.ImagePath = await UploadImageAsync(request.Image, role);
    }

    private void RemoveOldImageIfExists(TUser user)
    {
        if (user.ImagePath is null)
            return;

        string webRootPath = _locationService.GetWebRootPath();
        string filePath = Path.Combine(webRootPath, user.ImagePath);
        _fileService.DeleteFile(filePath);
    }

    private async Task<string> UploadImageAsync(IFormFile image, string role)
    {
        string webRootPath = _locationService.GetWebRootPath();
        string relativeFolderPath = $"Images/{role}s";
        return await _fileService.UploadFileAsync(image, webRootPath, relativeFolderPath);
    }

    private void HandleEmailChange(TUser user, bool isEmailChanged)
    {
        if (!isEmailChanged)
            return;

        user.EmailConfirmed = false;
    }

    private async Task SendConfirmationEmailIfNeeded(TUser user, bool isEmailChanged)
    {
        if (!isEmailChanged)
            return;

        await SendConfirmationEmail(user);
    }

    private async Task SendConfirmationEmail(TUser user)
    {
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string encodedToken = Utils.Encode(token);
        string confirmationUrl = GetConfirmationUrl(user.Id, encodedToken);
        (string subject, string message) = GetComposedEmailContent(user.UserName!, confirmationUrl);
        await _emailService.SendEmailAsync(user.Email!, message, subject);
    }

    private (string Subject, string Body) GetComposedEmailContent(string userName, string confirmationUrl)
    {
        string emailSubject = "Confirm your email";

        string emailBody = $"""
            <h1>Welcome {userName}</h1>

            <p>Thank you for registering.</p>

            <p>Please confirm your email address by clicking the link below:</p>

            <a href="{confirmationUrl}">
                Confirm Email
            </a>

            <p>If you did not create this account, ignore this email.</p>
            """;

        return (emailSubject, emailBody);
    }

    private string GetConfirmationUrl(int userId, string token)
    {
        string confirmEmailUrl = _urlService.GetConfirmEmailUrl();
        return $"{confirmEmailUrl}?userId={userId}&token={token}";
    }

    private async Task UploadImageIfExists(TUser user, TCommand request, string role)
    {
        if (request.Image is null)
            return;

        user.ImagePath = await UploadImageAsync(request.Image, role);
    }

    #endregion

    #region Protected Methods

    protected async Task<TUser> CreateAsync(TCommand request, string password, string role)
    {
        var user = _mapper.Map<TUser>(request);
        await UploadImageIfExists(user, request, role);
        await _userManager.AddAsync(user, password, role);
        await SendConfirmationEmail(user);

        return user;
    }

    protected async Task<TUser> UpdateAsync(int id, TCommand request, string role)
    {
        var user = (TUser)await _userManager.GetByIdAsync(id);
        bool isEmailChanged = IsEmailChanged(user, request);
        await UpdateImageAsync(user, request, role);
        _mapper.Map(request, user);
        HandleEmailChange(user, isEmailChanged);
        await _userManager.UpdateAsync(user);
        await SendConfirmationEmailIfNeeded(user, isEmailChanged);

        return user;
    }

    protected abstract Task<TUser> CreateOrUpdateUserAsync(TCommand request);

    #endregion

    #region Public Methods

    public async Task<Response<string>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await CreateOrUpdateUserAsync(request);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Success<string>(_localizer[SharedResourceKeys.SuccessfulOperation]);
    }

    #endregion
}