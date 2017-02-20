using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogiaWebForms.Models
{
    public class CatalogCustomField
    {
        [Key]
        public string CustomFieldId { get; set; }

        public string ObjectID { get; set; }

        public string Collector{ get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string SignedName { get; set; }
    }
}