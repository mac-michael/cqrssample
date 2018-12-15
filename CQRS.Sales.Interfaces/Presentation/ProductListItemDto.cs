using CQRS.Base.DDD.Domain.SharedKernel;

namespace CQRS.Sales.Interfaces.Presentation
{
    public class ProductListItemDto
    {
        public int ProductId { get; set; }
        public string DisplayedName { get; set; }
        public Money Price { get; set; }
    }
}