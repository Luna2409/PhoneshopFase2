using Phoneshop.Domain.Objects;
using System;
using System.Data;

namespace Phoneshop.Business.Extensions
{
    public static class PhoneExtensions
    {
        public static double PriceWithoutVat(this Phone phone)
        {
            double priceWithoutTax = phone.PriceWithTax / 1.21;

            return Math.Round(priceWithoutTax, 2);
        }
    }
}
