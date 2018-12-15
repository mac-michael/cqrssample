namespace CQRS.Base.DDD.Sagas
{
    public abstract class Saga
    {
        public bool IsCompleted { get; private set; }

        protected void MarkAsCompleted()
        {
            IsCompleted = true;
        }
    }

    public abstract class Saga<TData> : Saga
    {
        public TData Data { get; set; }
    }
}