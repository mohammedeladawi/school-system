using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Api.AppMetaData;

namespace SchoolProject.Api.Services;

public class UrlService : IUrlService
{
    #region Private Fields
    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion

    #region Constructors
    public UrlService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion

    #region Private Methods
    private string GetBaseUrl()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        string scheme = $"{request?.Scheme}";
        var host = $"{request?.Host}";

        return $"{scheme}://{host}";
    }
    #endregion

    #region Public Methods
    public string GetConfirmEmailUrl()
    {
        string baseUrl = GetBaseUrl();
        string path = Router.Authentication.ConfirmEmail;

        return $"{baseUrl}/{path}";
    }

    public string GetResetPasswordUrl()
    {
        string baseUrl = GetBaseUrl();
        string path = Router.Authentication.ResetPassword;

        return $"{baseUrl}/{path}";
    }
    #endregion
}