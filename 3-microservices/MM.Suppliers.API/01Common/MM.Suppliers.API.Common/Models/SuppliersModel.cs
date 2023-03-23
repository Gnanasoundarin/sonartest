using MM.Base.Core.Models;

namespace MM.Suppliers.API.Common.Models
{
    public class SuppliersModel : BaseModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public bool Active { get; set; }

    }
}
