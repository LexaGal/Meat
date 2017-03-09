using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataTypes;
using System.Globalization;
using System.Configuration;
using LifeSystem.LoadBalancer.DAL.Repositories;
using LifeSystem.LoadBalancer.DAL.Entities;

namespace LifeSystem.LoadBalancer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class LoadBalService : ILoadBalService
    {   
        public LoadBalService()
        {}

        public State RegisterHost(Host host)
        {
            //bool exist = false;

            HostRepository hostRepo = new HostRepository();
            //var hosts = hostRepo.GetAll();

            //foreach(var h in hosts)
            //{
            //    if (h.Ip == host.Ip && h.Port == host.Port)
            //    {
            //        exist = true;
            //    }
            //}

            //if (exist)
            //{
            //    return new State() {IsError = true, Message = "Host is already registred!" };
            //}

            //string id = Guid.NewGuid().ToString();
            HostEntity entity = new HostEntity {HostId = host.Id, Ip = host.Ip, Port = host.Port, IsBusy = host.IsBusy};

            hostRepo.Create(entity);
            //hostRepo.Save();

            return new State() {IsError = false, Message = "id"};
        }

        public State UnRegisterHost(string id)
        {
            HostRepository hostRepo = new HostRepository();
            var host = hostRepo.Get(id);

            if (host != null)
            {
                hostRepo.Delete(id);
                hostRepo.Save();   
                return new State() { IsError = false, Message = "Host is disconnected!" };
            }
            return new State() { IsError = true, Message = "There is no any host with this id!" };
        }

        public State PushResult(TaskResult result)
        {
            ResultRepository resultRepo = new ResultRepository();
            HostRepository hostRepo = new HostRepository();
            TaskRepository taskRepo = new TaskRepository();

            if (resultRepo.Get(result.HostId, result.TaskId, result.PartId) != null)
            {
                return new State() { IsError = true, Message = "Result was already pushed!" };
            }
            if (hostRepo.Get(result.HostId) == null)
            {
                return new State() { IsError = true, Message = "There is no any host with this id!" };
            }
            if (taskRepo.Get(result.HostId, result.TaskId) == null)
            {
                return new State() { IsError = true, Message = "There is no any task with this id for this host!" };
            }

            resultRepo.Create(new TaskResultEntity
            {
                HostId = result.HostId,
                TaskId = result.TaskId,
                PartId = result.PartId,
                //Array = result.Data.Array,
                Height = result.Data.Height,
                Width = result.Data.Width,
                StepFrom = result.StepFrom,
                StepTo = result.StepTo
            });
            resultRepo.Save();

            return new State() { IsError = false, Message = "Push was succesed complete!" };
        }

        public State SaveTask(Task task)
        {
            HostRepository hostRepo = new HostRepository();
            TaskRepository taskRepo = new TaskRepository();

            var host = hostRepo.Get(task.HostId);

            if (host == null)
                return new State { IsError = true, Message = "There is no any host with this id!" };
            if (host.IsBusy)
                return new State { IsError = true, Message = "This host is already busy!" };

            taskRepo.Create(new TaskEntity
            {
                HostId = task.HostId,
                TaskId = task.TaskId,
                //Array = task.Data.Array,
                Height = task.Data.Height,
                Width = task.Data.Width,
                StepsCount = task.StepsCount
            });
            taskRepo.Save();

            host.IsBusy = !(host.IsBusy);
            hostRepo.Update(host);
            hostRepo.Save();

            return new State { IsError = false, Message = "Task was saved succesfully!" };
        }
    }
}
