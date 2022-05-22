using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Common.ViewModels.RequestModels;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
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
    }
}
