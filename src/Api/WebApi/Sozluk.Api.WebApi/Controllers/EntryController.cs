using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Common.ViewModels.RequestModels.Entry;

namespace Sozluk.Api.WebApi.Controllers
{ 
    public class EntryController : BaseController
    {
        private readonly IMediator _mediator;

        public EntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create-entry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand entryCommand)
        {
            if (!entryCommand.CreatedById.HasValue)
            {
                entryCommand.CreatedById = UserId;
            }
            var result = await _mediator.Send(entryCommand);

            return Ok(result);
        }
        [HttpPost]
        [Route("create-entry-comment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand entryCommentCommand)
        {
            if (!entryCommentCommand.CreatedById.HasValue)
            {
                entryCommentCommand.CreatedById = UserId;
            }
            var result = await _mediator.Send(entryCommentCommand);

            return Ok(result);
        }
    }
}
