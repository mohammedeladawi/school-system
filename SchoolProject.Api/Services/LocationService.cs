using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Interfaces.ApiServices;
using SchoolProject.Api.AppMetaData;

namespace SchoolProject.Api.Services;

public class LocationService : ILocationService
{
    #region Private Fields
    private readonly IWebHostEnvironment _webHostEnvironment;
    #endregion

    #region Constructors
    public LocationService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    #endregion

    #region Public Methods
    public string GetWebRootPath()
    {
        return _webHostEnvironment.WebRootPath;
    }
    #endregion
}