//using DataTypes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Runtime.Serialization.Json;
//using System.Text;
//using System.Web.Script.Serialization;
//using LifeSystem.LoadBalancer;
//using System.Data.SqlClient;
//using LifeSystem.GameHost;
//using LifeSystem.LoadBalancer.DAL.Repositories;

//namespace LifeSystem.Tests
//{
//    [TestClass]
//    public class TaskTesting
//    {
//        private string _hostAddress;
//        private HostRepository _hostRepo = new HostRepository();
//        private TaskRepository _taskRepo = new TaskRepository();
//        private ResultRepository _resultRepo = new ResultRepository();

//        private Task InitializeTask()
//        {
//            var entity = _hostRepo.GetAll().LastOrDefault();

//            byte[] array = new byte[100];
//            Host host = new Host { Id = entity.HostId, Ip = entity.Ip, Port = entity.Port, IsBusy = entity.IsBusy };

//            for (int i = 0; i < 100; i++)
//                array[i] = 0;

//            array[34] = 1; array[44] = 1; array[54] = 1;
//            array[88] = 1; array[89] = 1;
//            array[98] = 1; array[99] = 1;

//            Task task = new Task()
//            {
//                HostId = host.Id,
//                TaskId = 1,
//                Data = new LifeData { Array = array, Height = 10, Width = 10 },
//                StepsCount = 1,
//            };

//            _hostAddress = "http://" + host.Ip + ":" + host.Port + "/HostService.svc/";
//            return task;
//        }

//        public TaskResult InitializeTaskResult(DataTypes.Task task)
//        {
//            byte[] array = new byte[100];

//            for (int i = 0; i < 100; i++)
//                array[i] = 0;

//            array[43] = 1; array[44] = 1; array[45] = 1;
//            array[88] = 1; array[89] = 1;
//            array[98] = 1; array[99] = 1;

//            TaskResult result = new TaskResult()
//            {
//                HostId = task.HostId,
//                TaskId = task.TaskId,
//                PartId = 1,
//                Data = new LifeData { Array = array, Height = 10, Width = 10 },
//                StepFrom = 0,
//                StepTo = 0
//            };
//            return result;
//        }

//        [TestMethod]
//        public void TestTask()
//        {
//            Task task = InitializeTask();
//            TaskResult result = InitializeTaskResult(task);

//            //TestSaveTask(task);
//            CalculateTask(task);
//            PushResult(task, result);
//        }

//        public void TestSaveTask(Task task)
//        {

//            Request request = new Request(_hostAddress);
           
//            int oldTasksCount = _taskRepo.GetAll().Count();

//            LoadBalService loadBalService = new LoadBalService();
//            State saveState = loadBalService.SaveTask(task);

//            JavaScriptSerializer js = new JavaScriptSerializer();
//            string postData = js.Serialize(task);
//            string data = request.CreateRequest(_hostAddress, postData, "task");
//            State state = request.GetState(data);

//            int newTasksCount = _taskRepo.GetAll().Count();

//            _hostRepo = new HostRepository();
//            var entity = _hostRepo.Get(task.HostId);

//            Assert.IsFalse(saveState.IsError);
//            Assert.IsFalse(state.IsError);
//            Assert.AreEqual(oldTasksCount + 1, newTasksCount);
//            Assert.IsTrue(entity.IsBusy);
//            Assert.IsFalse(state.IsError);

//            saveState = loadBalService.SaveTask(task);
//            Assert.IsTrue(saveState.IsError);
//            Assert.AreEqual("This host is already busy!", saveState.Message);

//            task.HostId = "7";
//            saveState = loadBalService.SaveTask(task);
//            Assert.IsTrue(saveState.IsError);
//            Assert.AreEqual("There no any host with this id!", saveState.Message);
//        }

//        public List<TaskResult> CalculateTask(Task task)
//        {
//            GameService service = new GameService();
//            List<TaskResult> results = service.CalculateTask(task);

//            TaskResult righResult = InitializeTaskResult(task);

//            Assert.AreEqual(1, results.Count);
//            Assert.AreEqual(righResult.HostId, results[0].HostId);
//            Assert.AreEqual(righResult.TaskId, results[0].TaskId);
//            Assert.AreEqual(righResult.PartId, results[0].PartId);
//            for (int i = 0; i < righResult.Data.Array.Length; i++)
//            {
//                Assert.AreEqual(righResult.Data.Array[i], results[0].Data.Array[i]);
//            };
//            Assert.AreEqual(righResult.Data.Height, results[0].Data.Height);
//            Assert.AreEqual(righResult.Data.Width, results[0].Data.Width);
//            Assert.AreEqual(righResult.StepFrom, results[0].StepFrom);
//            Assert.AreEqual(righResult.StepTo, results[0].StepTo);

//            return results;
//        }
       
//        public void PushResult(Task task, TaskResult result)
//        {
//            _hostAddress = "http://localhost:8080/Service.svc/";

//            Request request = new Request(_hostAddress);

//            int oldResultsCount = _resultRepo.GetAll().Count();

//            JavaScriptSerializer js = new JavaScriptSerializer();
//            string postData = js.Serialize(result);
//            string data = request.CreateRequest(_hostAddress, postData, "push");
//            State state = request.GetState(data);

//            int newResultsCount = _resultRepo.GetAll().Count();

//            Assert.AreEqual(oldResultsCount + 1, newResultsCount);
//            Assert.IsFalse(state.IsError);

//            data = request.CreateRequest(_hostAddress, postData, "push");
//            state = request.GetState(data);
//            Assert.IsTrue(state.IsError);
//            Assert.AreEqual("Result was already pushed!", state.Message);

//            result.TaskId = 100;
//            postData = js.Serialize(result);
//            data = request.CreateRequest(_hostAddress, postData, "push");
//            state = request.GetState(data);
//            Assert.IsTrue(state.IsError);
//            Assert.AreEqual("There are no any task with this id to this host!", state.Message);

//            result.HostId = "12";
//            postData = js.Serialize(result);
//            data = request.CreateRequest(_hostAddress, postData, "push");
//            state = request.GetState(data);
//            Assert.IsTrue(state.IsError);
//            Assert.AreEqual("There no any host with this id!", state.Message);

//            task = InitializeTask();
//            result = InitializeTaskResult(task);

//            _taskRepo.Delete(task.HostId, task.TaskId);
//            _taskRepo.Save();

//            _resultRepo.Delete(result.HostId, result.TaskId, result.PartId);
//            _resultRepo.Save();

//            var host = _hostRepo.Get(task.HostId);
//            host.IsBusy = false;
//            _hostRepo.Update(host);
//            _hostRepo.Save();
//        }
//    }
//}
