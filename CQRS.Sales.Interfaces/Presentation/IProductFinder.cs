using CQRS.Base.CQRS.Query;

namespace CQRS.Sales.Interfaces.Presentation
{
    public interface IProductFinder
    {
        PaginatedResult<ProductListItemDto> FindProducts(ProductSearchCriteria searchCriteria);
    }
}