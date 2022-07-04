using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Queries.GetEntries;
using Sozluk.Api.Application.Features.Queries.GetEntryComments;
using Sozluk.Api.Application.Features.Queries.GetEntryDetail;
using Sozluk.Api.Application.Features.Queries.GetMainPageEntries;
using Sozluk.Api.Application.Features.Queries.GetUserEntries;
using Sozluk.Common.ViewModels.Queries;
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
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetEntryDetailQuery(id, UserId));
            return Ok(result);
        }

        [HttpGet]
        [Route("comments/{id}")]
        public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
        {
            var result =  await _mediator.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("user-entries")]
        public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
            {
                userId = UserId.Value;
            }

            var result = await _mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var entries = await _mediator.Send(query);

            return Ok(entries);
        }

        [HttpGet]
        [Route("get-main-page-entries")]
        public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
        {
            var entries = await _mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));

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

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}