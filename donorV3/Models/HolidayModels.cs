using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace VOA.Models
{
    [Table("VOA_Holidays")]
    public class Holiday
    {
        public int ID { get; set; }
        [Column("Holiday_Name")]
        public string Name { get; set; }
        [Column("Holiday_Date")]
        public DateTime Date { get; set; }
    }

    public class HolidayDBContext : DbContext
    {
        public HolidayDBContext()
            : base("DefaultConnection")
        { }

        public DbSet<Holiday> Holidays { get; set; }
    }
}