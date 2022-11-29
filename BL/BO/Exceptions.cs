using System.Runtime.Serialization;
namespace BO 
{
    //exceptions that were thrown fron the DO
    [Serializable]
    public class BONotfoundException : Exception
    {
        //אין עניין להחזיק מלא בנאים בסופו של דבר החריגה של נתון לא נמצא תציד נזרקת מדי או 
        string throwing;
        public BONotfoundException(Exception? innerException, string? message = "") : base(message, innerException)
        {
            string throwing = innerException.ToString() + message;
        }
        public BONotfoundException(string massage) : base(massage) { throwing = "BL error! the " + massage+ " is not found."; }

        public override string ToString()
            => $@"{throwing}\n";
    }
    [Serializable]
    public class RepeatedUpdateBO : Exception
    {
        //אין עניין להחזיק מלא בנאים בסופו של דבר החריגה של נתון לא נמצא תציד נזרקת מדי או 
        string throwing;
        public RepeatedUpdateBO(Exception? innerException, string? message = "") : base(message, innerException)
        {
            string throwing = innerException.ToString() + message;
        }
        public override string ToString()
            => $@"{throwing}\n";
    }

    //exceptions that were thrown fron the BO
    [Serializable]
    public class DataMissingException : Exception
    {
        string throwing;
        public DataMissingException(string massage) : base(massage) { throwing = massage; }
        public override string ToString()
             => $@"BL: Error! {throwing} is missing to complete the operation";
    }   
    [Serializable]
    public class ItemMissingException : Exception
    {
        string throwing;     
        public ItemMissingException(string massage) 
            : base(massage) { throwing = $@"BL: The item {massage} is out of stock!"; }
        public ItemMissingException(string massage,int amountInTheStock) : 
            base(massage) { throwing =
                $@"BL: Missing items in stock! There are only {amountInTheStock} items left of the {massage}"; }
        public override string ToString()
             => throwing;
    }
    [Serializable]
    public class NoElementsException : Exception
    {
        string throwing;
        public NoElementsException(string massage) : base(massage) { throwing = massage; }
        public override string ToString()
             => $@"BL: No {throwing} to display\n";
    }
    [Serializable]
    public class InvalidInputBO : Exception
    {
        string throwing;
        public InvalidInputBO(string massage) : base(massage) { throwing = massage; }
        public override string ToString()
             => $@"BL Error! Invalid input of {throwing}\n";
    }

}

