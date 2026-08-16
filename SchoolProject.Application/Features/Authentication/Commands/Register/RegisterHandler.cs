using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.ApiServices;

namespace SchoolProject.Application.Features.Authentication.Commands.Register
{
    public class RegisterHandler : ResponseHandler, IRequestHandler<RegisterCommand, Response<string>>
    {
        #region Private Fields
        private readonly IUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUrlService _urlService;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructors
        public RegisterHandler(
            IUserManager userManager,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IUrlService urlService,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
            : base(localizer, mapper)
        {
            _userManager = userManager;
            _urlService = urlService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Private Methods
        private (string Subject, string Body) GetComposedEmailContent(
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

        private string GetConfirmationUrl(int userId, string token)
        {
            string confirmEmailUrl = _urlService.GetConfirmEmailUrl();
            return $"{confirmEmailUrl}?userId={userId}&token={token}";
        }

        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.Identities.ApplicationUser>(request);

            using (await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    // Add User To Db
                    await _userManager.AddAsync(user, request.Password);

                    // Send Confirmation Email
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string encodedToken = Utils.Encode(token);

                    string confirmationUrlTemplate = GetConfirmationUrl(user.Id, encodedToken);
                    (string subject, string message) = GetComposedEmailContent(user!.UserName!, confirmationUrlTemplate);

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
        }
        #endregion

    }
}
