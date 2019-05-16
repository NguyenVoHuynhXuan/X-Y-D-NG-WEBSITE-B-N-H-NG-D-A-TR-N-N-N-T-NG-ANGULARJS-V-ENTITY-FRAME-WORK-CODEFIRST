using uStora.Data.Infrastructure;
using uStora.Model.Models;

namespace uStora.Data.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
    }

    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}