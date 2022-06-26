using MediatR;

namespace Sozluk.Api.Application.Features.Command.User.ConfirmEmail;

public class ConfirmEmailCommand:IRequest<bool>
{
    public Guid ConfirmationId { get; set; }
}