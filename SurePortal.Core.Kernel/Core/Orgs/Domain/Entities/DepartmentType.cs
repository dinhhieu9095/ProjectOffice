
namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Entities
{
    public class DepartmentType : BaseEntity
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public bool IsActive { get; private set; }

        public static DepartmentType Create(string name, string code)
        {
            return new DepartmentType()
            {
                Name = name,
                Code = code
            };
        }

        public void Update(string name, string code, bool isActive)
        {
            Name = name;
            Code = code;
            IsActive = isActive;
        }
    }
}
