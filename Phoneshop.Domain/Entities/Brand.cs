using Phoneshop.Domain.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoneshop.Domain.Objects
{
    public class Brand : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Phone> Phones { get; set;}
    }
}
