using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Phoneshop.Business.Builders
{
    public class OrderBuilder : IOrderBuilder
    {
        private readonly Order _order = new();

        public OrderBuilder()
        {
        }

        public IOrderBuilder AddProduct(ProductOrder productOrder)
        {
            _order.ProductsPerOrder.Add(productOrder);
            return this;
        }

        public IOrderBuilder AddProducts(IEnumerable<ProductOrder> products)
        {
            foreach (var phone in products)
            {
                _order.ProductsPerOrder.Add(phone);
            }
            return this;
        }

        public Order Build() 
            => _order;

        public IOrderBuilder SetPrice(double price)
        {
            _order.TotalPrice = price;
            return this;
        }

        public IOrderBuilder SetUserId(string userId)
        {
            _order.CustomerId = userId;
            return this;
        }

        public IOrderBuilder SetVat(double vat)
        {
            _order.VatPercentage = vat;
            return this;
        }
    }
}
