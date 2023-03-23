using MM.Base.Core.Entities;

namespace MM.Suppliers.API.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public bool Active { get; set; }

    }
}
