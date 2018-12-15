namespace CQRS.CRM.Domain
{
    public interface ICustomerRepository
    {
        Customer Load(int id);
        void Save(Customer entity);
    }
}