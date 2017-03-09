using System.Runtime.Serialization;

namespace DataTypes
{
    [DataContract]
    public class State
    {
        public State()
        { }

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}