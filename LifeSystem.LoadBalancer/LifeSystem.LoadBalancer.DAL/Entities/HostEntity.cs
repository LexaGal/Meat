using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSystem.LoadBalancer.DAL.Entities
{
    public class HostEntity
    {
        [Key]
        public string HostId { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public bool IsBusy { get; set; }
    }
}
