using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Phoneshop.Business.Scrapers
{
    public class BelsimpelScraper : IScraper
    {
        //https://www.belsimpel.nl/API

        public bool CanExecute(string url)
        {
            return url.StartsWith(@"C:\temp\Belsimpel");
        }

        public IEnumerable<Phone> Execute(string url)
        {
            throw new NotImplementedException();
        }
    }
}
