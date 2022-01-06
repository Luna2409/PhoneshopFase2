using Phoneshop.Domain.Interfaces;

namespace Phoneshop.Domain.Objects
{
    public class Phone : IEntity
    {
        public int Id { get; set; }
        public int BrandID { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double PriceWithTax { get; set; }
        public double PriceWithoutTax { get; set; }
        public int Stock { get; set; }
        public string FullName 
        { 
            get { return $"{Brand} - {Type}"; } 
        }
    }
}
