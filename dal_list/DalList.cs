using DalApi;
namespace Dal
{
    internal sealed class DalList : IDal
    {      
        private DalList()
        { 
            Iorder = new DalOrder();
            Iorderitem = new DalOrderItem();
            IProduct = new DalProduct();
        }
        public static IDal Instance { get; } = new DalList();
        public Iorder Iorder { get; }
        public Iorderitem Iorderitem { get; }   
        public IProduct IProduct { get; }
    }
}