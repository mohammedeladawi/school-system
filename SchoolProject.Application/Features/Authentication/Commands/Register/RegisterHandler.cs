using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Shared.AppMetaData;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Helpers;

namespace SchoolProject.Application.Features.Authentication.Commands.Register
{
    public class RegisterHandler : ResponseHandler, IRequestHandler<RegisterCommand, Response<string>>
    {
        #region Private Fields
        private readonly IUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructors
        public RegisterHandler(
            IUserManager userManager,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
            : base(localizer, mapper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Private Methods
        private (string Subject, string Body) GetComposedEmailContent(
            Domain.Entities.Identities.ApplicationUser user,
            string token,
            string confirmationUrlTemplate)
        {
            string confirmationUrl = string.Format(confirmationUrlTemplate, user.Id, token);
            string emailSubject = "Confirm your email";
            string emailBody = $"""
                <h1>Welcome {user.UserName}</h1>

                <p>Thank you for registering.</p>

                <p>Please confirm your email address by clicking the link below:</p>

                <a href="{confirmationUrl}">
                    Confirm Email
                </a>

                <p>If you did not create this account, ignore this email.</p>
                """;

            return (emailSubject, emailBody);
        }

        private string GetConfirmationUrlTemplate()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";
            string path = Router.Authentication.ConfirmEmail;

            return $"{baseUrl}/{path}?userId={{0}}&token={{1}}";
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

                    string confirmationUrlTemplate = GetConfirmationUrlTemplate();
                    (string subject, string message) = GetComposedEmailContent(user, encodedToken, confirmationUrlTemplate);

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
