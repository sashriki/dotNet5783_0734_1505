using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Product : IProduct
{
    private IDal Dal = new DalList();

}
