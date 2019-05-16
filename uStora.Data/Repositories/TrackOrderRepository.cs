using System;
using System.Collections;
using System.Collections.Generic;
using uStora.Data.Infrastructure;
using uStora.Model.Models;
using System.Linq;
using uStora.Common.FunctionsCommon;

namespace uStora.Data.Repositories
{
    public interface ITrackOrderRepository : IRepository<TrackOrder>
    {
        IEnumerable<TrackOrder> GetLocation(string cusId);
    }

    public class TrackOrderRepository : RepositoryBase<TrackOrder>, ITrackOrderRepository
    {
        public TrackOrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<TrackOrder> GetLocation(string cusId)
        {
            var query = from od in DbContext.Orders
                        join tod in DbContext.TrackOrders
                        on od.ID equals tod.OrderId
                        where od.CustomerId == cusId
                        && (od.PaymentStatus == 1 || od.PaymentStatus == 0)
                        select tod;
            query = query.Distinct(x => x.UserId).AsQueryable();
            return query;
        }
    }
}