namespace Catalog.API.Models;

// Default constructor for Mapster
public record Pagination<T>()
{
    public Pagination(
        int totalItems,
        int pageSize) : this()
    {
        this.totalItems = totalItems;
        this.pageSize = pageSize;
    }

    public int totalItems { get; init; }

    public int pageSize { get; init; }

    public int totalPages { get => (int)Math.Ceiling(totalItems / (double)pageSize); }

    private int _pageIndex;
    public int pageIndex
    {
        get => _pageIndex == totalPages ? _pageIndex : _pageIndex + 1;
        init => _pageIndex = value;
    }

    public IEnumerable<T> items { get; set; } = [];
}
