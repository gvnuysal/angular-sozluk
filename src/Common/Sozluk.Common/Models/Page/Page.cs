namespace Sozluk.Common.ViewModels.Page;

public class Page
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalRowCount { get; set; }
    public int TotalPageCount => (int)Math.Ceiling((double)TotalRowCount / PageSize);
    public int Skip { get; set; }
}