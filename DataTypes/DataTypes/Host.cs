using System.Runtime.Serialization;

namespace DataTypes
{
    //serialize
    [DataContract]
    public class Host
    {
        public Host(int id, string ip, int port, bool isBusy)
        {
            Id = "Host" + id;
            Ip = ip;
            Port = port.ToString();
            IsBusy = isBusy;
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Ip { get; set; }

        [DataMember]
        public string Port { get; set; }

        [DataMember]
        public bool IsBusy { get; set; }
    }
}