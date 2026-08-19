using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Application.Features.ApplicationUser.Commands;

namespace SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate
{
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

        #region Protected Methods
        protected (string Subject, string Body) GetComposedEmailContent(
            string userName,
            string confirmationUrl)
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

        protected string GetConfirmationUrl(int userId, string token)
        {
            string confirmEmailUrl = _urlService.GetConfirmEmailUrl();
            return $"{confirmEmailUrl}?userId={userId}&token={token}";
        }


        protected void RemoveOldImage(TCommand request, TUser user)
        {
            string webRootPath = _locationService.GetWebRootPath();
            string filePath = Path.Combine(webRootPath, user!.ImagePath!);
            _fileService.DeleteFile(filePath);
        }

        /// <summary>
        /// Abstract method to add a user to the database with the specified role.
        /// </summary>
        protected abstract Task<TUser> CreateOrUpdateUserAsync(TCommand request);

        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var user = await CreateOrUpdateUserAsync(request);

                // Send Confirmation Email
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string encodedToken = Utils.Encode(token);
                string confirmationUrl = GetConfirmationUrl(user.Id, encodedToken);
                (string subject, string message) = GetComposedEmailContent(user!.UserName!, confirmationUrl);
                await _emailService.SendEmailAsync(user.Email!, message, subject);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            return Created<string>(_localizer[SharedResourceKeys.AddedSuccessfully]);
        }
        #endregion
    }
}
