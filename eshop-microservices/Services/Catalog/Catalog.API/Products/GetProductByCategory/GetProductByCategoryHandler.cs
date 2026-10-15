
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category):IQuery<GeProductByCategoryResult>;
    public record GeProductByCategoryResult(IEnumerable<Product> products);
    internal class GetProductByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GeProductByCategoryResult>
    {
        public async Task<GeProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(p => p.Category.Contains(query.category)).ToListAsync();
            return new GeProductByCategoryResult(products);

        }
    }
}
