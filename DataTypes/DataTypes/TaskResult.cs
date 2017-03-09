using System.Runtime.Serialization;

namespace DataTypes
{
    [DataContract]
    public class TaskResult
    {
        [DataMember]
        public string HostId { get; set; }

        [DataMember]
        public int TaskId { get; set; }

        [DataMember]
        public int PartId { get; set; }

        [DataMember]
        public LifeData Data { get; set; }

        [DataMember]
        public int StepFrom { get; set; }

        [DataMember]
        public int StepTo { get; set; }
    }
}