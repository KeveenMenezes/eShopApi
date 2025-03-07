namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(
    int PageIndex,
    int PageSize)
    : IQuery<GetProductsResult>;

public record GetProductsResult(Pagination<Product> ProductPagination);

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var pagination = new Pagination<Product>(
            await session.Query<Product>().CountAsync(cancellationToken),
            request.PageSize
        )
        {
            pageIndex = request.PageIndex
        };

        pagination.items = await session.Query<Product>()
            .Skip(pagination.pageSize * (pagination.pageIndex - 1))
            .Take(pagination.pageSize)
            .ToListAsync(cancellationToken);

        return new GetProductsResult(pagination);
    }
}
