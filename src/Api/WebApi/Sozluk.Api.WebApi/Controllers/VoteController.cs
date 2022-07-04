using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Command.Entry.DeleteVote;
using Sozluk.Api.Application.Features.Command.EntryComment.DeleteVote;
using Sozluk.Api.WebApi.Controllers;
using Sozluk.Common.ViewModels;
using Sozluk.Common.ViewModels.RequestModels.Entry;

[Route("api/[controller]")]
[ApiController]
public class VoteController : BaseController
{
    private readonly IMediator mediator;

    public VoteController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("Entry/{entryId}")]
    public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
    {
        var result = await mediator.Send(new CreateEntryVoteCommand(entryId, UserId.Value,voteType));

        return Ok(result);
    }

    [HttpPost]
    [Route("EntryComment/{entryCommentId}")]
    public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
    {
        var result = await mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId.Value));

        return Ok(result);
    }

    [HttpPost]
    [Route("DeleteEntryVote/{entryId}")]
    public async Task<IActionResult> DeleteEntryVote(Guid entryId)
    {
        await mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

        return Ok();
    }

    [HttpPost]
    [Route("DeleteEntryCommentVote/{entryId}")]
    public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
    {
        await mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

        return Ok();
    }
}