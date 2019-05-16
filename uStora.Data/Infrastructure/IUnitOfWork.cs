namespace uStora.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}