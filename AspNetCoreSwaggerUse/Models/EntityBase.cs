using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSwaggerUse.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}