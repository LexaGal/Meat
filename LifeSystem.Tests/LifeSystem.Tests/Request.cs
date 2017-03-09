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
        private string _hostAddress;

        public Request(string hostAddress)
        {
            _hostAddress = hostAddress;
        }

        public string CreateRequest(string data, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_hostAddress + "api/" + method);

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
