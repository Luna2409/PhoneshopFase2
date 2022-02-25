using Phoneshop.Domain.Entities;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Phoneshop.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class PhoneExtensions
    {
        public static double PriceWithoutVat(this Phone phone)
        {
            double priceWithoutTax = phone.PriceWithTax / 1.21;

            return Math.Round(priceWithoutTax, 2);
        }
    }
}
