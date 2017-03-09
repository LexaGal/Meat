using LifeSystem.LoadBalancer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSystem.LoadBalancer.DAL.Repositories
{
    public class HostRepository : IRepository<HostEntity>
    {
        public ServiceContext _context;

        public HostRepository()
        {
            _context = new ServiceContext();
        }

        public IEnumerable<HostEntity> GetAll()
        {
            return _context.Hosts;
        }

        public HostEntity Get(string id)
        {
            HostEntity host = _context.Hosts.FirstOrDefault(h => h.HostId == id);
            return host;
        }

        public void Create(HostEntity item)
        {
            _context.Hosts.Add(item);
        }

        public void Update(HostEntity item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(string id)
        {
            HostEntity host = _context.Hosts.FirstOrDefault(h => h.HostId == id);
            if (host != null)
                _context.Hosts.Remove(host);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
