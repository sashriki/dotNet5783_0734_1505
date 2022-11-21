

namespace DO
{
    public class NotfoundException : Exception
    {
        public NotfoundException() : base() { }
        public NotfoundException(string massage) : base(massage) { }
        public override string ToString()
             => $@"Error! The object was not found";
    }
    public class DuplicationException : Exception
    {
        public DuplicationException() : base() { }
        public DuplicationException(string massage) : base(massage) { }
        public override string ToString()
             => $@"Error! The object already exists in the system";
    }
}