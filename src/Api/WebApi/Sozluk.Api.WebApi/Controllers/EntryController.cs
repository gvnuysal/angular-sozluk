using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Queries.GetEntries;
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

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery]GetEntriesQuery query)
        {
            var entries = await _mediator.Send(query);

            return Ok(entries);
        }
        [HttpPost]
        [Route("create-entry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand entryCommand)
        {
            entryCommand.CreatedById ??= UserId;
            var result = await _mediator.Send(entryCommand);

            return Ok(result);
        }
        [HttpPost]
        [Route("create-entry-comment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand entryCommentCommand)
        {
            entryCommentCommand.CreatedById ??= UserId;
            var result = await _mediator.Send(entryCommentCommand);

            return Ok(result);
        }
    }
}
