using System;
using System.Collections.Generic;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Sales.Interfaces.Domain;

namespace CQRS.Sales.Interfaces.Presentation
{
    public class ClientOrderDetailsDto
    {
        public int OrderId { get; set; }
        public Money TotalCost { get; set; }

        /**
         * TODO change to a type from presentation model
         */
        public List<OrderedProduct> OrderedProducts { get; set; }

        /**
         * TODO change to a type from presentation model
         */
        public OrderStatus OrderStatus { get; set; }

        public DateTime SubmitDate { get; set; }
    }
}