using System.ComponentModel.DataAnnotations;

namespace MatBlazor.Model.Entity
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
