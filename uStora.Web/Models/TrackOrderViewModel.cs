using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uStora.Model.Models;

namespace uStora.Web.Models
{
    public class TrackOrderViewModel
    {
        public int ID { get; set; }

        public int OrderId { get; set; }

        public int VehicleId { get; set; }
        
        public string UserId { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public bool Status { get; set; }

        public bool isDone { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public virtual OrderViewModel Order { get; set; }
        
        public virtual VehicleViewModel Vehicle { get; set; }
    }
}