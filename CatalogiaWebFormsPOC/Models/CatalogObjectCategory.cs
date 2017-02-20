using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatalogiaWebForms.Models
{
    public class CatalogObjectCategory
    {
        [Key, ScaffoldColumn(false)]
        public int CategoryID { get; set; }

        [Required, StringLength(50), Display(Name = "Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Object Description")]
        public string ObjectDescription { get; set; }

        public virtual ICollection<CatalogObject> CatalogObjects { get; set; }
    }
}