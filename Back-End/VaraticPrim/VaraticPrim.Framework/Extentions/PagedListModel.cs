namespace VaraticPrim.Framework.Extentions;

public class PagedListModel<T>
{
    public T[] Data       { get; set;}
    public int PageIndex  { get; set;}
    public int PageSize   { get; set;}
    public int TotalCount { get; set;}
    public int TotalPages { get; set;}
    
    public PagedListModel()
    {
    }

    public PagedListModel(T[] data, int pageIndex, int pageSize, int totalCount, int totalPages)
    {
        Data = data;
        TotalCount = totalCount;
        TotalPages = totalPages;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}