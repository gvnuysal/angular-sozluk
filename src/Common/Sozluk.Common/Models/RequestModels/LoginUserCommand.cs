using MediatR;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Common.ViewModels.RequestModels;

public class LoginUserCommand:IRequest<LoginUserViewModel>
{
    public string EmailAddress { get; set; }
    public string Password { get;  set; }

    public LoginUserCommand()
    {
        
    }
    public LoginUserCommand(string email, string password)
    {
        EmailAddress = email;
        Password = password;
    }


}