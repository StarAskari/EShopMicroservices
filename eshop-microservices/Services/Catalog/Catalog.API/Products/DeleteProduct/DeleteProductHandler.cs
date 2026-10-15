
using Catalog.API.Exceptions;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {

            RuleFor(p => p.id).NotEmpty().WithMessage("Id must not be empty");
        }

        internal class DeleteProductCommandHandler(IDocumentSession session)
            : ICommandHandler<DeleteProductCommand, DeleteProductResult>
        {
            public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
            {
                var product = await session.LoadAsync<Product>(command.id, cancellationToken);
                if (product != null)
                {
                    throw new ProductNotFoundException(command.id);
                }
                session.Delete<Product>(command.id);
                session.SaveChangesAsync(cancellationToken);

                return new DeleteProductResult(true);

            }
        }
    }
}
