using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using idtiApiService.Model;

namespace idtiApiService.Impl
{
    public class IntegrationContext : DbContext
    {

        public DbSet<Terminal> terminals { get; set; }
        public DbSet<EventData> eventlogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=idti.db");
        }

    }
}
