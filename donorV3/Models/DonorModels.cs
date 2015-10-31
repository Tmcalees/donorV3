using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace VOA.Models
{
    [Table("VOA_Donor")]
    public class Donor
    {
        public int ID { get; set; }
        [Column("Last_Name")]
        public string LastName { get; set; }
        [Column("First_Name")]
        public string FirstName { get; set; }
        [Column("House_Number")]
        public string HouseNumber { get; set; }
        [Column("Street1")]
        public string Street1 { get; set; }
        [Column("Street2")]
        public string Street2 { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("State")]
        public string State { get; set; }
        [Column("ZipCode")]
        public string ZipCode { get; set; }
        [Column("Telephone")]
        public string Telephone { get; set; }
        [Column("Notes")]
        public string Notes { get; set; }
        [Column("AltPhone")]
        public string AltPhone { get; set; }
        [Column("Email")]
        public string Email { get; set; }
    }

    public class DonorDBContext : DbContext
    {
        public DonorDBContext()
            : base("DefaultConnection")
        { }

        public DbSet<Donor> Donors   { get; set; }
    }
}
