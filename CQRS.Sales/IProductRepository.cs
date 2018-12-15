namespace CQRS.Erp.Sales
{
    public interface IProductRepository
    {
        Product Load(int id);
    }
}