using Phoneshop.Domain.Entities;
using System.Collections.Generic;

namespace Phoneshop.Domain.Interfaces
{
    public interface IScraper
    {
        bool CanExecute(string url);

        IEnumerable<Phone> Execute(string url);
    }
}
