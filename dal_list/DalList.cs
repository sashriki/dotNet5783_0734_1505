using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DO;
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