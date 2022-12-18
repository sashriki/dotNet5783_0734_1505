

namespace DO
{
    [Serializable]
    public class NotfoundException : Exception
    {
        public NotfoundException(string massage) : base(massage) { }
        public override string ToString()
             => $@"Dal Error! The {this.Message} was not found";
    }
    [Serializable]
    public class DuplicationException : Exception
    {
        public DuplicationException(string massage) : base(massage) { }
        public override string ToString()
             => $@"Dal Error! The {this.Message} already exists in the system";
    }
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}