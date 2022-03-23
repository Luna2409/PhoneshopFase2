using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Phoneshop.Business.Builders
{
    public class OrderBuilder : IOrderBuilder
    {
        private readonly Order _order;

        public OrderBuilder()
        {
            _order = new Order();
        }

        public IOrderBuilder AddProduct(ProductOrder productOrder)
        {
            throw new NotImplementedException();
        }

        public IOrderBuilder AddProducts(IEnumerable<ProductOrder> products)
        {
            throw new NotImplementedException();
        }

        public Order Build() 
            => _order;

        public IOrderBuilder SetPrice(double price)
        {
            throw new NotImplementedException();
        }

        public IOrderBuilder SetUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public IOrderBuilder SetVat(double vat)
        {
            throw new NotImplementedException();
        }
    }
}
