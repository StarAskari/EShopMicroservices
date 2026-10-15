
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdRequest();
    public record GetProductByIdResponse(Product product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async(Guid id,ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProductById")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get ProductById")
                .WithDescription("Get ProductById");
        }
    }
}
