using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogiaWebForms.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public string Username { get; set; }

        [Required(ErrorMessage = "Lastname cannot be empty")]
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Firstname cannot be empty")]
        [DisplayName("First Name")][StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City cannot be empty")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage = "State cannot be empty")]
        [StringLength(20)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code cannot be empty")]
        [StringLength(11)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country cannot be empty")]
        [StringLength(30)]
        public string Country { get; set; }
        
        [StringLength(12)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,5}",
        ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        [ScaffoldColumn(false)]
        public string PaymentTransactionId { get; set; }

        [ScaffoldColumn(false)]
        public bool HasBeenShipped { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}