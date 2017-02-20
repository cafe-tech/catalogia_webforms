
using System.ComponentModel.DataAnnotations;

namespace CatalogiaWebForms.Models
{
    public class CartItem
    {
        [Key]
        public string ItemId { get; set; }
        public string CartId { get; set; }
        public string ItemDescription { get; set; }
        private int _quantity = 0;
        public int Quantity 
        {
            get
            {

                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        public System.DateTime DateCreated { get; set; }
        // public string ObjectId { get; set; }
        public virtual CatalogObject CatalogObject { get; set; }
    }
}