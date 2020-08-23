using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatBlazor.Models.Entity
{
    [Table("users")]
    public class User : EntityBase
    {
        public string Name { get; set; }

        public string Sex { get; set; }

        public DateTime Birthday { get; set; }

        [Column("phone_num")]
        public string PhoneNum { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
