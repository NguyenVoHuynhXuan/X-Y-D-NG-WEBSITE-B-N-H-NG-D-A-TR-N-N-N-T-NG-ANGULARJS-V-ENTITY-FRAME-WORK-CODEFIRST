namespace uStora.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private uStoraDbContext dbContext;

        public uStoraDbContext Init()
        {
            return dbContext ?? (dbContext = new uStoraDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}