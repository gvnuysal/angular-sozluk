using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Command.User.ConfirmEmail;
using Sozluk.Common.Events.User;
using Sozluk.Common.ViewModels.RequestModels;

namespace Sozluk.Api.WebApi.Controllers
{ 
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var ok = await _mediator.Send(command);
            return Ok(ok);
        }

        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(Guid id)
        {
            var ok = await _mediator.Send(new ConfirmEmailCommand { ConfirmationId = id });
            return Ok(ok);
        }
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ChangeUserPasswordCommand changeUserPasswordCommand)
        {
            if (!changeUserPasswordCommand.UserId.HasValue)
            {
                changeUserPasswordCommand.UserId = UserId;
            }
            var ok = await _mediator.Send(changeUserPasswordCommand);
            return Ok(ok);
        }
    }
}