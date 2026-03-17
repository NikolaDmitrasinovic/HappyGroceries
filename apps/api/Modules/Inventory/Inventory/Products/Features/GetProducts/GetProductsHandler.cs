using Shared.Pagination;

namespace Inventory.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<ProductDto> Products);

internal class GetProductsHandler(InventoryDbContext dbContext)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);

        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var productDtos = new List<ProductDto>();
        foreach (var product in products)
        {
            var productDto = new ProductDto(product.Name, product.Stock, product.Threshold);
            productDtos.Add(productDto);
        }

        return new GetProductsResult(
            new PaginatedResult<ProductDto>(
                pageIndex,
                pageSize,
                totalCount,
                productDtos)
            );
    }
}
