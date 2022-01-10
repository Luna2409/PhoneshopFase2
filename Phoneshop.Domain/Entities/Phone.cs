using Phoneshop.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoneshop.Domain.Objects
{
    public class Phone : IEntity
    {
        public int Id { get; set; }
        public int BrandID { get; set; }

        [NotMapped]
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double PriceWithTax { get; set; }
        public double PriceWithoutTax { get; set; }
        public int Stock { get; set; }
        //public Brand Brand { get; set; }
        public string FullName 
        { 
            get { return $"{Brand} - {Type}"; } 
        }
    }
}
