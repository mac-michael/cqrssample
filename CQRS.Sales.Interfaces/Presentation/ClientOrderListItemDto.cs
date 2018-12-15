using System;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Sales.Interfaces.Domain;

namespace CQRS.Sales.Interfaces.Presentation
{
    public class ClientOrderListItemDto 
    {
        public int OrderId { get; set; }
        public Money TotalCost { get; set; }
        public DateTime SubmitDate { get; set; }
        public OrderStatus Status { get; set; }

        public ClientOrderListItemDto() 
        {
        }

        public ClientOrderListItemDto(int orderId, Money totalCost, DateTime submitDate, OrderStatus status)
        {
            OrderId = orderId;
            TotalCost = totalCost;
            SubmitDate = submitDate;
            Status = status;
        }
    }
}