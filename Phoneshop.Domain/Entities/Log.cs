using Phoneshop.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Domain.Entities
{
    public class Log : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Level { get; set; }

        public string Message { get; set; } 

        public DateTime DateTime { get; set; }
    }
}
