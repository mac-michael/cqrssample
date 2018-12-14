using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.Infrastructure.NHibernate.Repositories;
using CQRS.Sales.Domain;
using NHibernate.Linq;

namespace CQRS.Sales.Infrastructure.Repositories
{
    public class MyResource : IDisposable
    {
        public void Close()
        {
            
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }

    [DomainRepositoryImplementation]
    public class ProductRepository : GenericRepositoryForBaseEntity<Product>, IProductRepository
    {
        public Product GetByName(string name)
        {
            return EntityManager.CurrentSession.Query<Product>()
                .FirstOrDefault(p => p.Name == name);
        }
    }
}