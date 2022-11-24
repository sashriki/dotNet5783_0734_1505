using DalApi;
namespace Dal
{
    sealed public class DalList : IDal
    {
        public Iorder Iorder => new DalOrder();
        public Iorderitem Iorderitem => new DalOrderItem();
        public IProduct IProduct => new DalProduct();
    }
}