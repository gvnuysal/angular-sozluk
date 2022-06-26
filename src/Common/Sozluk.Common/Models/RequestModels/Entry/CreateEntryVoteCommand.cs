﻿using MediatR;

namespace Sozluk.Common.ViewModels.RequestModels.Entry;

public class CreateEntryVoteCommand:IRequest<bool>
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType VoteType { get; set; }
}