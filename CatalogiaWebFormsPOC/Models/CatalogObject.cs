using System.ComponentModel.DataAnnotations;

namespace CatalogiaWebForms.Models
{
    public class CatalogObject
    {
        string _catalogObjectId;

        [Key, ScaffoldColumn(false)]
        public string ObjectId
        {
            get
            {
                if (string.IsNullOrEmpty(_catalogObjectId))
                {
                    _catalogObjectId = System.Guid.NewGuid().ToString();
                }
                return _catalogObjectId;
            }
            set
            {
                _catalogObjectId = value;
            }
        }


        [Required, StringLength(50)]
        public string ObjectName { get; set; }

        [StringLength(50)]
        public string OtherName { get; set; }

        // public int? Quantity { get; set; }

        private double? _price;
        [Display(Name = "Price")]
        public double? Price // { get; set; }
        {
            get
            {
                if (_price == null)
                {
                    _price = 0;
                }
                return _price;
            }
            set
            {
                _price = value;
            }
        }

        public string ImagePath { get; set; }

        public int CategoryID { get; set; }

        // public virtual CatalogObjectCategory[] CatalogObjectCategory { get; set; }

        // public virtual CatalogCustomField[] CatalogCustomFields { get; set; }
    }



    
}