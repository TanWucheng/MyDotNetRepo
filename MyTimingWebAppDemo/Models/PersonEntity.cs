using System.Linq;
using MyTimingWebAppDemo.Utils;

namespace MyTimingWebAppDemo.Models
{
    public class PersonEntity : IEntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNum { get; set; }

        public override string ToString()
        {
            var dic = ReflectPropertyUtil<PersonEntity>.ReflectGetPropertyKeyValuePairs(this);
            var list = dic.Select(o => $"\"{o.Key}\":\"{o.Value}\"").ToList();
            return $"{{{string.Join(",", list)}}}";
        }
    }
}