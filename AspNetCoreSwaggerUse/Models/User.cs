namespace AspNetCoreSwaggerUse.Models
{
    public class User : EntityBase
    {
        public string Name { get; set; }

        public Sex Sex { get; set; }

        public int Age { get; set; }
    }
}