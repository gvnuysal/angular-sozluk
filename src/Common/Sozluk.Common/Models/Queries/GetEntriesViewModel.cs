﻿namespace Sozluk.Common.ViewModels.Queries;

public class GetEntriesViewModel
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public int  CommentCount { get; set; }
}