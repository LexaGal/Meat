using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using DataTypes;

namespace LifeSystem.GameHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHostService" in both code and config file together.
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/api/ping",
            ResponseFormat = WebMessageFormat.Json)]
        bool Ping(string hostId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/api/task",
           ResponseFormat = WebMessageFormat.Json)]
        List<TaskResult> CalculateTask(Task task);

        //[OperationContractAttribute(AsyncPattern = true)]
        //IAsyncResult BeginServiceAsyncMethod(string msg, AsyncCallback callback, object asyncState);

        //// Note: There is no OperationContractAttribute for the end method.
        //string EndServiceAsyncMethod(IAsyncResult result);
    }
}
