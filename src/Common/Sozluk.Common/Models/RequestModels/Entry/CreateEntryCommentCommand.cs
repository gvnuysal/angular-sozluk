﻿using MediatR;

namespace Sozluk.Common.ViewModels.RequestModels.Entry;

public class CreateEntryCommentCommand:IRequest<Guid>
{
    public Guid EntryId { get; set; }
    public string Content { get; set; }
    public Guid CreatedById { get; set; }

    public CreateEntryCommentCommand()
    {
        
    }
    public CreateEntryCommentCommand(Guid entryId, string content, Guid createdById)
    {
        EntryId = entryId;
        Content = content;
        CreatedById = createdById;
    }
}