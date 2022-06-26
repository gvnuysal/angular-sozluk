namespace Sozluk.Common.Events.Entry;

public class DeleteVoteEntryEvent
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
}