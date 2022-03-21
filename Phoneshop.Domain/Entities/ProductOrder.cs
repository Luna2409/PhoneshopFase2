using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoneshop.Domain.Entities
{
    public class ProductOrder
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Phone")]
        public int PhoneId { get; set; }

        public Phone Phone { get; set; }

        public int Amount { get; set; }
    }
}
