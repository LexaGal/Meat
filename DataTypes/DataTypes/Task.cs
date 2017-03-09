using System.Runtime.Serialization;

namespace DataTypes
{
    [DataContract]
    public class Task
    {
        public Task(int taskId, string hostId, LifeData data, int stepsCount)
        {
            HostId = hostId;
            TaskId = taskId;
            Data = data;
            StepsCount = stepsCount;
        }

        [DataMember]
        public string HostId { get; set; }

        [DataMember]
        public int TaskId { get; set; }

        [DataMember]
        public LifeData Data { get; set; }

        [DataMember]
        public int StepsCount { get; set; }
    }
}