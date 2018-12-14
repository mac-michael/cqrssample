using NHibernate.Persister.Entity;

namespace CQRS.Sales.Domain
{
    public interface IProductRepository
    {
        Product Load(int id);
        Product GetByName(string name);
    }
}