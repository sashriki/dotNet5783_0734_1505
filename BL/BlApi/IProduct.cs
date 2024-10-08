﻿namespace BlApi;
public interface IProduct
{
    public IEnumerable<BO.ProductForList?> getAllProducts(Func<BO.ProductForList?, bool>? condition = null);
    public BO.Product getByIdToMannage(int id);
    public BO.ProductItem getByIdToCostumer(int id, BO.Cart cart);
    public IEnumerable<BO.ProductForList> GetAllByCondition(Func<BO.ProductForList?, bool>? condition, IEnumerable<BO.ProductForList> productForLists);
    public IEnumerable<BO.ProductItem> GetAllToCastumer(BO.Cart cart);
    public IEnumerable<BO.ProductItem> GetAllByConditionToCastumer(Func<BO.ProductItem?, bool>? condition, IEnumerable<BO.ProductItem> ProductItems);
    public void addProduct (BO.Product product);
    public void removeProduct (int id);
    public void updateProduct (BO.Product product);
}
