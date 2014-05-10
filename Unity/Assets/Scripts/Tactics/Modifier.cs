namespace Tactics
{
    [System.Serializable]
    public class Modifier : System.Object
    {
        int value;
        int originatorID;
        string originatorType;
        
        public int Value { get { return value; } }
        public int OriginatorID { get { return originatorID; } }
        public string OriginatorType { get { return originatorType; } }
        

        public Modifier(int value_, int originatorID_, string originatorType_)
        {
            value = value_;
            originatorID = originatorID_;
            originatorType = originatorType_;
        }
    }
}
