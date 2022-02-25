using Phoneshop.Domain.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Domain.Entities
{
    public class Brand : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Phone> Phones { get; set; }
    }
}
