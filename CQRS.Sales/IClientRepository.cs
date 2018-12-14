namespace CQRS.Erp.Sales
{
    public interface IClientRepository
    {
        Client Load(int clientId);
    }
}