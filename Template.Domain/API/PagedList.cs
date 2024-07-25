namespace Template.Domain.API;
public class PagedList<T>
{
    public List<T> Items { get; set; }
    public PageInfo PageInfo { get; set; }

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        PageInfo = new PageInfo
        {
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            PageSize = pageSize,
            TotalCount = count
        };

        Items = items;
    }
}

public class PageInfo
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}