using VaraticPrim.Repository.Paged;

namespace VaraticPrim.Framework.Extentions;

public static class PagedListMappingExtensions
{
    public static PagedListModel<TOutput> Map<TInput, TOutput>(this PagedList<TInput> pagedList, Func<TInput, TOutput> func) where TInput: class where TOutput: class
    {
        return new PagedListModel<TOutput>(pagedList.Select(func).ToArray(), pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount, pagedList.TotalPages);
    }
 
    public static async Task<PagedListModel<TOutput>> MapAsync<TInput, TOutput>(this PagedList<TInput> pagedList, Func<TInput, Task<TOutput>> asyncFunc) where TInput: class where TOutput: class
    {
        var mapped = new List<TOutput>();
 
        foreach (var item in pagedList)
        {
            mapped.Add(await asyncFunc(item));
        }
 
        return new PagedListModel<TOutput>(mapped.ToArray(), pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount, pagedList.TotalPages);
    }
}