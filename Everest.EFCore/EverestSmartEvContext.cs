using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Everest.EFCore
{
   public  class EverestSmartEvContext : DbContext
    {
        public EverestSmartEvContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ChargeStation> ChargeStations { get; set; }
        public DbSet<Connector> Connectors { get; set; }


    }
}
