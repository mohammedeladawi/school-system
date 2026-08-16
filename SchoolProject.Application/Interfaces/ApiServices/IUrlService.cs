namespace SchoolProject.Application.Interfaces.ApiServices;

public interface IUrlService
{
    string GetConfirmEmailUrl();
    string GetResetPasswordUrl();
}