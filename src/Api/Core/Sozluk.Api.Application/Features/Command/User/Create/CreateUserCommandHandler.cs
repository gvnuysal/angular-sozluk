using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common;
using Sozluk.Common.Events.User;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Infrastructure.Exceptions;
using Sozluk.Common.ViewModels.RequestModels;

namespace Sozluk.Api.Application.Features.Command.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await _userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);
        if (existsUser is not null)
        {
            throw new DatabaseValidationException("User allready exists");
        }

        var dbUser = _mapper.Map<Domain.Models.User>(request);
        var rows = await _userRepository.AddAsync(dbUser);
        if (rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = request.EmailAddress
            };

            QueryFactory.SendMessageToExchange(SozlukConstants.UserExchangeName, SozlukConstants.DefaultExchangeType,
                SozlukConstants.UserEmailChangedQueueName, @event);
        }
         

        return dbUser.Id;
    }
}