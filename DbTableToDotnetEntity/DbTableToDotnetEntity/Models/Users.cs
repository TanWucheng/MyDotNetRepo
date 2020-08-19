using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbTableToDotnetEntity.Models
{
    [Table("users")]
    public class Users : BaseModel
    {
        public string Name { get; set; }

        public string Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        [Column("phone_num")]
        public string PhoneNum { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
