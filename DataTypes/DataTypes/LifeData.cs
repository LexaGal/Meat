using System.Runtime.Serialization;

namespace DataTypes
{
    [DataContract]
    public class LifeData
    {
        public LifeData() { }

        public LifeData(byte[] array, bool isFirst, bool isLast, int width, int height)
        {
            Array = array;
            IsFirst = isFirst;
            IsLast = isLast;
            Height = height;
            Width = width;
        }

        [DataMember]
        public bool IsFirst { get; set; }

        [DataMember]
        public bool IsLast { get; set; }

        [DataMember]
        public byte[] Array { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public int Width { get; set; }
    }
}
