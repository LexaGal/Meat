using LifeSystem.LoadBalancer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSystem.LoadBalancer.DAL.Repositories
{
    public class ResultRepository : IRepository<TaskResultEntity>
    {
        public ServiceContext _context;

        public ResultRepository()
        {
            _context = new ServiceContext();
        }

        public TaskResultEntity Get(string hostId, int taskId, int partId)
        {
            return _context.Results.FirstOrDefault(t => (t.HostId == hostId && t.TaskId == taskId && t.PartId == partId));
        }

        public IEnumerable<TaskResultEntity> GetAll()
        {
            return _context.Results;
        }

        public void Create(TaskResultEntity item)
        {
            _context.Results.Add(item);
        }

        public void Update(TaskResultEntity item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(string hostId, int taskId, int partId)
        {
            TaskResultEntity result =
                _context.Results.FirstOrDefault(t => (t.HostId == hostId && t.TaskId == taskId && t.PartId == partId));
            if (result != null)
                _context.Results.Remove(result);
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
