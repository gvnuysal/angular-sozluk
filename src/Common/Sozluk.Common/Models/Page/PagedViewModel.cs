namespace Sozluk.Common.ViewModels.Page;

public class PagedViewModel<T> where T : class
{
    public IList<T> Results { get; set; }
    public Page PageInfo { get; set; }

    public PagedViewModel() : this(new List<T>(), new Page())
    {
    }

    public PagedViewModel(IList<T> results, Page pageInfo)
    {
        Results = results;
        PageInfo = pageInfo;
    }
}