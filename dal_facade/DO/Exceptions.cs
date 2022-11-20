//לבדוק נושא זריקת חריגות

namespace DO
{
    public class Notfound : Exception
    {
        public Notfound() : base() { }
        public override string ToString()
             => $@"Error! The object was not found";
    }
    public class duplication : Exception
    {
        public duplication() : base() { }
        public override string ToString()
             => $@"Error! The object already exists in the system";
    }
}