using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace VOA.Models
{
    [Table("VOA_Pickup_Locations")]
    public class PickupLocation
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
    }

    public class PickupLocationDBContext : DbContext
    {
        public PickupLocationDBContext()
            : base("DefaultConnection")
        { }

        public DbSet<PickupLocation> PickupLocations { get; set; }
    }
}