namespace Catalog.API.Models;

public record Pagination<T>(
    int pageSize,
    int totalItems
)
{
    // Default constructor for Mapster
    public Pagination() : this(0, 0) { }

    public int totalPages { get => (int)Math.Ceiling(totalItems / (double)pageSize); }

    private int _pageIndex;
    public int pageIndex
    {
        get => _pageIndex == totalPages ? _pageIndex : _pageIndex + 1;
        init => _pageIndex = value;
    }

    public IEnumerable<T> items { get; set; } = [];
}
