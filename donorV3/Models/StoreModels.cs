using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace VOA.Models
{
    [Table("VOA_Stores")]
    public class Store
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

    public class StoreDBContext : DbContext
    {
        public StoreDBContext()
            : base("DefaultConnection")
        { }

        public DbSet<Store> Stores { get; set; }
    }
}