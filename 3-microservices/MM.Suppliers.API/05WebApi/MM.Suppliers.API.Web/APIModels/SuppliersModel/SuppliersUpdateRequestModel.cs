using System.ComponentModel.DataAnnotations;

namespace MM.Suppliers.API.Web.APIModels
{
    public class SuppliersUpdateRequestModel
    {
        [Required(ErrorMessage = "Supplier Name is required!!"), StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Maximum length for the Supplier Name is 50!!")]

        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Contact Person is required!!"), StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "Maximum length for the Contact Person is 100!!")]

        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Email is required!!"), StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "Maximum length for the ContactEmail is 100!!")]
        [DataType(DataType.EmailAddress)] 
        public string ContactEmail { get; set; }
        public bool Active { get; set; }
    }
}
