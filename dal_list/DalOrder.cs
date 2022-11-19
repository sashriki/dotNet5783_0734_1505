﻿using DO;
namespace Dal;
using DalApi;


internal class DalOrder : Iorder
{
    /// <summary>
    /// Creating a new order and adding it to the list of orders
    /// </summary>
    /// <param name="NewOrder"></param>
    /// <returns></returns>
    public int Add(Order NewOrder)
    {
        NewOrder.OrderId = DataSource.Config.orderIndex;
        DataSource.orders.Add(NewOrder);
        return NewOrder.OrderId;
    }

    /// <summary>
    /// Returning the order list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetAll()
    {
        List<Order> orderReturnList = new List<Order>();
        for (int i = 0; i < DataSource.orders.Count(); i++) //warning??
            orderReturnList.Add(DataSource.orders[i]);
        return orderReturnList;
    }

    /// <summary>
    /// Returning an order from the order list
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order GetById(int idOrder)
    {
        int x = DataSource.orders.FindIndex(x => x.OrderId  == idOrder);
        if (x == -1)
            throw new Notfound();
        else
            return DataSource.orders[x];
    }

    /// <summary>
    /// Updating an order in the order list
    /// </summary>
    /// <param name="UpdatedOrder"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order UpdatedOrder)
    {
        int x = DataSource.orders.FindIndex(x => x.OrderId == UpdatedOrder.OrderId);
        if (x == -1)
            throw new Notfound();
        DataSource.orders[x] = UpdatedOrder;
    }

    /// <summary>
    /// Deleting an order from the order list
    /// </summary>
    /// <param name="removeById"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int removeById)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].OrderId == removeById)
            {
                DataSource.orders.Remove(DataSource.orders[i]);
                return;
            }
        }
        throw new Notfound();
    }
}
