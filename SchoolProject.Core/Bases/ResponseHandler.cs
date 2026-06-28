using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Bases;

public class ResponseHandler
{
    #region Protected Fields
    protected readonly IStringLocalizer<SharedResource> _localizer;
    protected readonly IMapper _mapper;

    #endregion

    #region Constructors
    public ResponseHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper)
    {
        _localizer = localizer;
        _mapper = mapper;
    }
    #endregion

    #region Public Methods
    public Response<T> Deleted<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = message ?? _localizer[SharedResourceKeys.DeletedSuccessfully].Value
        };
    }

    public Response<T> Success<T>(T entity, object? Meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = _localizer[SharedResourceKeys.SuccessfulOperation].Value,
            Meta = Meta
        };
    }

    public Response<T> Unauthorized<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Succeeded = false,
            Message = message ?? _localizer[SharedResourceKeys.Unauthorized].Value
        };
    }

    public Response<T> BadRequest<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = message ?? _localizer[SharedResourceKeys.BadRequest].Value
        };
    }

    public Response<T> Conflict<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.Conflict,
            Succeeded = false,
            Message = message ?? _localizer[SharedResourceKeys.Conflict].Value
        };
    }

    public Response<T> UnprocessableEntity<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = message ?? _localizer[SharedResourceKeys.UnprocessableEntity].Value
        };
    }

    public Response<T> NotFound<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,
            Succeeded = false,
            Message = message ?? _localizer[SharedResourceKeys.NotFound].Value
        };
    }

    public Response<T> Created<T>(string? message = null, object? Meta = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.Created,
            Succeeded = true,
            Message = message ?? _localizer[SharedResourceKeys.CreatedSuccessfully].Value,
            Meta = Meta
        };
    }

    #endregion
}