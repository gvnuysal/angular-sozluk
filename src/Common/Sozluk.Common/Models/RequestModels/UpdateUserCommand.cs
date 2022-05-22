using MediatR;

namespace Sozluk.Common.ViewModels.RequestModels;

public class UpdateUserCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string UserName { get; set; } 
}