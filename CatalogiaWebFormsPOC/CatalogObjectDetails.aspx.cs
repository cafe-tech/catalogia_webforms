using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CatalogiaWebForms.Models;
using System.Web.ModelBinding;

namespace CatalogiaWebForms
{
    public partial class CatalogObjectDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public IQueryable<CatalogObject> GetCatalogObjectDetails([QueryString("ObjectId")] string id)
        {
            var _db = new CatalogiaWebForms.Models.CatalogObjectContext();
            IQueryable<CatalogObject> query = _db.CatalogObjects;

            if (!string.IsNullOrEmpty( id))
            {
                query = query.Where(q => q.ObjectId == id);
            }
            else
            {
                query = null;
            }

            return query;
        }

        
    }
}