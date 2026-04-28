using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Bases;

public class ResponseHandler
{
    protected readonly IStringLocalizer<SharedResource> _localizer;
    public ResponseHandler(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
    }

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
            Message = _localizer[SharedResourceKeys.AddedSuccessfully].Value,
            Meta = Meta
        };
    }

    public Response<T> Unauthorized<T>()
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Succeeded = false,
            Message = _localizer[SharedResourceKeys.Unauthorized].Value
        };
    }

    public Response<T> BadRequest<T>(string? Message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = Message ?? _localizer[SharedResourceKeys.BadRequest].Value
        };
    }

    public Response<T> UnprocessableEntity<T>(string? Message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = Message ?? _localizer[SharedResourceKeys.UnprocessableEntity].Value
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

    public Response<T> Created<T>(T entity, object? Meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.Created,
            Succeeded = true,
            Message = _localizer[SharedResourceKeys.CreatedSuccessfully].Value,
            Meta = Meta
        };
    }
}