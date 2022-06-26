using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common;
using Sozluk.Common.Events.User;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Infrastructure.Exceptions;
using Sozluk.Common.ViewModels.RequestModels;

namespace Sozluk.Api.Application.Features.Command.User.Update;

public class UpdateUserCommandHandler:IRequestHandler<UpdateUserCommand,bool>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _userRepository.GetByIdAsync(request.Id);
       
        if (dbUser is null)
        {
            throw new DatabaseValidationException("User not found");
        }
        var dbEmailAddress = dbUser.EmailAddress;
        var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;
        _mapper.Map(request, dbUser);
        
        var rows = await _userRepository.UpdateAsync(dbUser);
        //check if email changed
        if (emailChanged&& rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = dbEmailAddress,
                NewEmailAddress = request.EmailAddress
            };

            QueueFactory.SendMessageToExchange(SozlukConstants.UserExchangeName, SozlukConstants.DefaultExchangeType,
                SozlukConstants.UserEmailChangedQueueName, @event);
            dbUser.EmailConfirmed = false;
            await _userRepository.UpdateAsync(dbUser);
        }
        return rows>0;
    }
}