namespace DalApi
{
    public interface IDal
    {
        Iorder Iorder { get; }
        Iorderitem Iorderitem { get; }
        IProduct IProduct { get; }
    }
}
