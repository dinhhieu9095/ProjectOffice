using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Core.Kernel.Orgs.Domain.Entities
{
    [Table("SystemConfigs", Schema = "Core")]
    public class SystemConfig
    {


        [Key]
        public string Code { get; private set; }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public static SystemConfig Create(string code, string name, string value)
        {
            return new SystemConfig()
            {
                Code = code,
                Name = name,
                Value = value,
            };
        }
        public void Update(string name, string value)
        {

            Name = name;
            Value = value;

        }
    }
}
