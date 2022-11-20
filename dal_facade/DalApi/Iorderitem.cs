using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
using DO;
public interface Iorderitem : Icrud<OrderItem>
{
    public OrderItem GetbyIdOfProductAndOrder(int idOrder, int idProduct);
}
