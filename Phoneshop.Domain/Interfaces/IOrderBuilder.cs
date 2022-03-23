using Phoneshop.Domain.Entities;
using System.Collections.Generic;

namespace Phoneshop.Domain.Interfaces
{
    public interface IOrderBuilder
    {
        Order Build();

        IOrderBuilder SetPrice(double price);

        IOrderBuilder SetVat(double vat);

        IOrderBuilder AddProduct(ProductOrder productOrder);

        IOrderBuilder AddProducts(IEnumerable<ProductOrder> products);

        IOrderBuilder SetUserId(string userId);
    }
}
