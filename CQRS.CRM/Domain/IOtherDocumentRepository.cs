namespace CQRS.CRM.Domain
{
    public interface ILeadRepository
    {
        void Save(Lead document);
    }
}