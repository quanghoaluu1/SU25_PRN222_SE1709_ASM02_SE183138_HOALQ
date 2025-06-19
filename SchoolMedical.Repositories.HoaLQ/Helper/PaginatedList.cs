namespace SchoolMedical.Repositories.HoaLQ.Helper;

public class PaginatedList<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    
    public PaginatedList(List<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
}