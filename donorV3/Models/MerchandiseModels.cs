using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace VOA.Models
{
    [Table("VOA_Merchandise")]
    public class Merchandise
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool CheckBox { get; set; }
        public bool Active { get; set; }
    }

        public class MerchandiseDBContext : DbContext
    {
        public MerchandiseDBContext()
            : base("DefaultConnection")
        { }

        public DbSet<Merchandise> Merchandise { get; set; }
    }
}