using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VOA.Models
{
    [Table("VOA_PickupSchedule")]
    public class ZipCode
    {
        public int ID { get; set; }
        [Column("ZipCode")]
        public string Code { get; set; }
        public string Town { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public int WeekOfMonth { get; set; }

        public bool HasPickup()
        {
            if (Monday == true || Tuesday == true || Wednesday == true || Thursday == true || Friday == true || Saturday == true || Sunday == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ZipCodeDBContext : DbContext
    {
        public ZipCodeDBContext()
            : base("DefaultConnection")
        { }

        public DbSet<ZipCode> ZipCodes { get; set; }        
    }
}