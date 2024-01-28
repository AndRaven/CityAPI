public class PaginationMetadata {

    public int TotalItems { get; set; }

    public int TotalPages { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public PaginationMetadata(int totalItems, int pageSize, int currentPage)
    {
        TotalItems = totalItems;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
    }
}