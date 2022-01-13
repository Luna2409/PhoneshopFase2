using Phoneshop.Domain.Interfaces;

namespace Phoneshop.Domain.Objects
{
    public class Brand : IEntity
    {
        public int Id { get; set; }

        public string BrandName { get; set; }
    }
}
