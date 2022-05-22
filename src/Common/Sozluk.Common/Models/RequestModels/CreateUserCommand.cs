using MediatR;

namespace Sozluk.Common.ViewModels.RequestModels;

public class CreateUserCommand:IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}