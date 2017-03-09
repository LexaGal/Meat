using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSystem.LoadBalancer.DAL.Entities
{
    public class TaskEntity
    {
        [Key, Column(Order = 1)]
        public string HostId { get; set; }
        [Key, Column(Order = 2)]
        public int TaskId { get; set; }
        public byte[] Array { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int StepsCount { get; set; }
    }
}
