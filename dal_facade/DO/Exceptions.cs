

namespace DO
{
    public class NotfoundException : Exception
    {
        public NotfoundException(string massage) : base(massage) { }
        public override string ToString()
             => $@"Dal Error! The {this.Message} was not found";
    }
    public class DuplicationException : Exception
    {
        public DuplicationException(string massage) : base(massage) { }
        public override string ToString()
             => $@"Dal Error! The {this.Message} already exists in the system";
    }
}