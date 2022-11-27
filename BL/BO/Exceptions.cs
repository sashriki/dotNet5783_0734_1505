﻿
namespace BO
{
    [Serializable]
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
    public class DataMissingException : Exception
    {
        public DataMissingException() : base() { }
        public DataMissingException(string massage) : base(massage) { }
        public override string ToString()
             => $@"Error! Data is missing to complete the operation";
    }
    public class ItemMissingException : Exception
    {
        public ItemMissingException() : base() { }
        public ItemMissingException(string massage) : base(massage) { }
        public override string ToString()
             => $@"The item is out of stock!";
    }
}
