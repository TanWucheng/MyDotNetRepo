using System;
using System.ComponentModel.DataAnnotations;

namespace MatBlazor.Models.Entity
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
