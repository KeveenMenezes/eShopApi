namespace Catalog.API.Products.GetProducts;

public record GetPagitationProductsResponse(
    Pagination<Product> ProductPagination);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/products/{pageIndex}/{pageSize}",
            async (
                int pageIndex,
                int pageSize,
                ISender sender,
                CancellationToken cancellationToken) =>
        {
            var query = new GetProductsQuery(pageIndex, pageSize);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetPagitationProductsResponse>();

            return Results.Ok(response.ProductPagination);
        })
        .WithName("GetProducts")
        .Produces<GetPagitationProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get all products");
    }
}
