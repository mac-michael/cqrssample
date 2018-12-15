using System;
using System.Linq;
using CQRS.Base.CQRS.Query;
using CQRS.Base.CQRS.Query.Attributes;
using CQRS.Base.Infrastructure.NHibernate;
using CQRS.Sales.Domain;
using CQRS.Sales.Interfaces.Presentation;
using NHibernate.Linq;

namespace CQRS.Sales.Presentation.Implementation
{
    [Finder]
    public class SqlProductFinder : IProductFinder
    {
        private const int MaxItemPerPage = 50;

        public IEntityManager EntityManager { get; set; }

        public PaginatedResult<ProductListItemDto> FindProducts(ProductSearchCriteria criteria)
        {
            criteria.ItemsPerPage = Math.Max(criteria.ItemsPerPage, MaxItemPerPage);

            int productsCount = CountProducts(criteria);
            if (productsCount <= 0)
                return new PaginatedResult<ProductListItemDto>(criteria.PageNumber, criteria.ItemsPerPage);

            var s = EntityManager.CurrentSession;
            var x = from p in ApplyCriteria(s.Query<Product>(), criteria)
                    select new ProductListItemDto
                               {
                                   DisplayedName = p.Name,
                                   Price = p.Price,
                                   ProductId = p.Id
                               };

            x = x.Skip(criteria.PageNumber*criteria.ItemsPerPage).Take(criteria.ItemsPerPage);

            return new PaginatedResult<ProductListItemDto>(x.ToList(), criteria.PageNumber,
                                                           criteria.ItemsPerPage, productsCount);
        }

        private int CountProducts(ProductSearchCriteria criteria)
        {
            return ApplyCriteria(EntityManager.CurrentSession.Query<Product>(), criteria).Count();
        }

        private IQueryable<Product> ApplyCriteria(IQueryable<Product> query, ProductSearchCriteria criteria)
        {
            // WHERE
            if (!string.IsNullOrEmpty(criteria.ContainsText))
                query = query.Where(x => x.Name.Contains(criteria.ContainsText));
            if (criteria.MaxPrice != null)
                query = query.Where(x => x.Price.Value <= criteria.MaxPrice.Value);
            if (criteria.HasSpecificProductIdsFilter())
                query = query.Where(x => criteria.SpecificProductIds.Contains(x.Id));

            // ORDER BY
            if (criteria.OrderBy == ProductSearchOrder.None)
                ;
            else if (criteria.OrderBy == ProductSearchOrder.Name)
            {
                if ( criteria.Ascending)
                    query = query.OrderBy(x => x.Name);
                else
                    query = query.OrderByDescending(x => x.Name);
            }
            else if (criteria.OrderBy == ProductSearchOrder.Price)
            {
                if (criteria.Ascending)
                    query = query.OrderBy(x => x.Price);
                else
                    query = query.OrderByDescending(x => x.Price);
            }
            else
                throw new ArgumentException("Unknown ORDER BY in ProductSearchCriteria");

            return query;
        }
    }
}