namespace VaraticPrim.Repository.Paged;

public abstract class PagedFilter
{
    public int PageIndex { get; set; } = 0;
    public int PageSize  { get; set; } = int.MaxValue;
}