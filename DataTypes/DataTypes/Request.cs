using DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace LifeSystem.Tests
{
    public class Request
    {
        private string _hostAddress = "http://localhost:8081/GameService.svc/";
        HttpWebRequest _request;
        string _data;

        public Request(string hostAddress)
        {
            _hostAddress = hostAddress;
        }

        public string CreateRequest(string data, string method)
        {
            _request = (HttpWebRequest)WebRequest.Create(_hostAddress + "api/" + method);

            _request.Method = "POST";
            _request.ContentType = "application/json";
            _request.ContentLength = data.Length;

            using (StreamWriter requestWriter = new StreamWriter(_request.GetRequestStream()))
            {
                requestWriter.Write(data);
            }

            var res = _request.BeginGetResponse(new AsyncCallback(FinishWebRequest), null);
            while (!res.IsCompleted)
            {}

            return _data;
        }

        void FinishWebRequest(IAsyncResult result)
        {
            try
            {
                var resp = _request.EndGetResponse(result);
                using (StreamReader responseReader = new StreamReader(resp.GetResponseStream()))
                //_request.GetResponse().GetResponseStream()))
                {
                    _data = responseReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                _data = null;
            }
        }

        public string CreateRequest(string hostAddress, string data, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hostAddress + "api/" + method);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            try
            {
                using (StreamWriter requestWriter = new StreamWriter(request.GetRequestStream()))
                {
                    requestWriter.Write(data);
                }

                using (StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    data = responseReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                data = null;
            }

            return data;
        }

        public State GetState(string data)
        {
            State state = new State();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(state.GetType());
            state = serializer.ReadObject(ms) as State;
            ms.Close();

            return state;
        }
    }
}
