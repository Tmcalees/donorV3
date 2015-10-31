using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VOA.Models
{
    [Table("VOA_Config")]
    public class ConfigData
    {
        [Column("ConfigKey")]
        public string ID { get; set; }
        [Column("ConfigValue")]
        public string Value { get; set; }
    }

    public class ConfigDataDBContext : DbContext
    {
        public ConfigDataDBContext()
            : base("DefaultConnection")
        { }

        public DbSet<ConfigData> ConfigData { get; set; }
    }
}