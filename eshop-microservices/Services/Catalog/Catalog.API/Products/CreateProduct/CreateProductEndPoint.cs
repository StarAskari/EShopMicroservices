namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid id);

    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>(); // mpaster converts CreateProductRequest to CreateProductCommand
                var result = await sender.Send(command); // mediatR sends the request(execute the handler)
                var response = result.Adapt<CreateProductResponse>(); // mapster converts CreateProductResult to CreateProductResponse
                return Results.Created($"/Products/{response.id}", response);// the output will be created
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
