//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net;
//using System.Web.Script.Serialization;
//using System.IO;
//using DataTypes;
//using System.Text;
//using System.Runtime.Serialization.Json;
//using System.Data.SqlClient;
//using System.Linq;
//using LifeSystem.LoadBalancer.DAL.Repositories;

//namespace LifeSystem.Tests
//{
//    [TestClass]
//    public class ConnectionTesting
//    {
//        private string _hostAddress = "http://localhost:8080/Service.svc/";
//        string ip = "localhost", port = "3000";
//        [TestMethod]
//        public void TestConnect()
//        {
//            HostRepository hostRepo = new HostRepository();
//            Request request = new Request(_hostAddress);

//            var hosts = hostRepo.GetAll();
//            int oldHostsCount = hosts.Count();

//            Host host = new Host
//            {
//                Ip = ip,
//                Port = port
//            };

//            JavaScriptSerializer js = new JavaScriptSerializer();

//            string postData = js.Serialize(host);
//            string data = request.CreateRequest(postData, "connect");
//            State state = request.GetState(data);
//            hosts = hostRepo.GetAll();
//            int newHostsCount = hosts.Count();

//            Assert.AreEqual(oldHostsCount + 1, newHostsCount);
//            Assert.IsNotNull(hosts.FirstOrDefault(h => h.Ip == ip && h.Port == port));
//            Assert.IsFalse(state.IsError);

//            hosts = hostRepo.GetAll();
//            oldHostsCount = hosts.Count();
//            data = request.CreateRequest(postData, "connect");
//            state = request.GetState(data);
//            hosts = hostRepo.GetAll();
//            newHostsCount = hosts.Count();

//            Assert.AreEqual(oldHostsCount, newHostsCount);
//            Assert.IsTrue(state.IsError);           
//        }

//        [TestMethod]
//        public void TestDisconnect()
//        {
//            HostRepository hostRepo = new HostRepository();
//            Request request = new Request(_hostAddress);
//            JavaScriptSerializer js = new JavaScriptSerializer();

//            var hosts = hostRepo.GetAll();
//            int oldHostsCount = hosts.Count();

//            string id = hosts.FirstOrDefault(h => h.Ip == ip && h.Port == port).HostId;
//            string postData = js.Serialize(id);
//            string data = request.CreateRequest(postData, "disconnect");
//            State state = request.GetState(data);

//            int newHostsCount = hostRepo.GetAll().Count();

//            Assert.AreEqual(oldHostsCount - 1, newHostsCount);
//            Assert.IsFalse(state.IsError);

//            oldHostsCount = hostRepo.GetAll().Count();

//            postData = js.Serialize(id);
//            data = request.CreateRequest(postData, "disconnect");
//            state = request.GetState(data);

//            newHostsCount = hostRepo.GetAll().Count();

//            Assert.AreEqual(oldHostsCount, newHostsCount);
//            Assert.IsTrue(state.IsError);
//        }   
//    }
//}
