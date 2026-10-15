
using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool isSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator() { 
            RuleFor(p=>p.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(p=>p.Name).NotEmpty().WithMessage("Name is required")
                .Length(5,100).WithMessage("Name must be between 5 to 100 charachter");
            RuleFor(p => p.Price).GreaterThan(100).WithMessage("Price must be higher than 100");
        }
    }
    public class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product == null) {
                throw new ProductNotFoundException(command.Id);
            }
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;

            session.Update(product);
            session.SaveChangesAsync();

            return new UpdateProductResult(true);


        }
    }
}
