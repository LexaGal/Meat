using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DataTypes;

namespace LifeSystem.LoadBalancer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ILoadBalService
    {
        [OperationContract]
        [WebInvoke(Method="POST", UriTemplate = "/api/connect", 
            ResponseFormat = WebMessageFormat.Json)]
        State RegisterHost(Host host);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/api/disconnect",
            ResponseFormat = WebMessageFormat.Json)]
        State UnRegisterHost(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/api/push",
            ResponseFormat = WebMessageFormat.Json)]
        State PushResult(TaskResult result);
    }
}
