namespace CQRS.Base.Infrastructure.Attributes
{
    public enum ComponentLifestyle
    {
        Singleton,
        Transient,
        PerRequest,
        PerSession
    }
}