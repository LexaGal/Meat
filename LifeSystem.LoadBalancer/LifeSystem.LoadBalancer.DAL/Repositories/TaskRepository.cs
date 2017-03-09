using LifeSystem.LoadBalancer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSystem.LoadBalancer.DAL.Repositories
{
    public class TaskRepository : IRepository<TaskEntity>
    {
        public ServiceContext _context;

        public TaskRepository()
        {
            _context = new ServiceContext();
        }

        public TaskEntity Get(string hostId, int taskId)
        {
            return _context.Tasks.FirstOrDefault(t => (t.HostId == hostId && t.TaskId == taskId));
        }

        public IEnumerable<TaskEntity> GetAll()
        {
            return _context.Tasks;
        }

        public void Create(TaskEntity item)
        {
            _context.Tasks.Add(item);
        }

        public void Update(TaskEntity item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(string hostId, int taskId)
        {
            TaskEntity task =
                _context.Tasks.FirstOrDefault(t => (t.HostId == hostId && t.TaskId == taskId));
            if (task != null)
                _context.Tasks.Remove(task);
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
