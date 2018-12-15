namespace CQRS.Sales.Domain
{
    public interface IClientRepository
    {
        Client Load(int clientId);
    }
}