using LifeSystem.LoadBalancer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSystem.LoadBalancer.DAL
{
    public class ServiceContext : DbContext
    {
        public ServiceContext()
            : base("DbConnection")
        { }

        public DbSet<HostEntity> Hosts { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskResultEntity> Results { get; set; }
    }
}
