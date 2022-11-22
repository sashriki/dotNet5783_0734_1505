using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class NotfoundExceptionBO : Exception
    {
        public NotfoundExceptionBO() : base() { }
        public NotfoundExceptionBO(string massage) : base(massage) { }
        public override string ToString()
             => $@"Error! The object was not found";
    }
    public class InvalidInputBO : Exception
    {
        public InvalidInputBO() : base() { }
        public InvalidInputBO(string massage) : base(massage) { }
        public override string ToString()
             => $@"Error! Invalid input";
    }
    public class RepeatedUpdateBO : Exception
    {
        public RepeatedUpdateBO() : base() { }
        public RepeatedUpdateBO(string massage) : base(massage) { }
        public override string ToString()
             => $@"The object has already been updated in the system";
    }
}
/*

namespace DO
{//Invalid input
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
}*/