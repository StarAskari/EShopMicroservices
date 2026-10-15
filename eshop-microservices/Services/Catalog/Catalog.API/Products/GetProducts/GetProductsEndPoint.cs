
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsRequest(int? pageNumber = 1, int? pageSize = 10);
    public record GetProductResponse(IEnumerable<Product> products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery(request.pageNumber, request.pageSize));
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");
        }

    }
}
