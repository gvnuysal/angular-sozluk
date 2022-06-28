using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Events.User;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Infrastructure.Exceptions;

namespace Sozluk.Api.Application.Features.Command.User.ChangePassword;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
            throw new ArgumentNullException(nameof(request.UserId));

        var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);

        if (dbUser is null)
        {
            throw new DatabaseValidationException("User Not Found");
        }

        var encryptedPassword = PasswordEncrytor.Encrpt(request.OldPassword);
        if (dbUser.Password != encryptedPassword)
        {
            throw new DatabaseValidationException("Old password wrong");
        }

        dbUser.Password =  PasswordEncrytor.Encrpt(request.NewPassword);;
        await _userRepository.UpdateAsync(dbUser);

        return true;
    }
}