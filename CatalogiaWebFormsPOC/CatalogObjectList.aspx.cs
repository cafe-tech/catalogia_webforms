using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CatalogiaWebForms.Models;
using System.Web.ModelBinding;
using System.Web.Routing;

namespace CatalogiaWebForms
{
    public partial class CatalogObjectList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public IQueryable<CatalogObject> GetCatalogItems([QueryString("id")] int? categoryId, [RouteData] string categoryName)
        {
            var _db = new CatalogiaWebForms.Models.CatalogObjectContext();

            IQueryable<CatalogiaWebForms.Models.CatalogObject> query = _db.CatalogObjects;

            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryID == categoryId);
            }

            if (!String.IsNullOrEmpty(categoryName))
            {
                int? categoryIdToFind = null;
                // Need to get CategoryId from CatalogObjectCategory
                CatalogObjectCategory extractedCategory = _db.CatalogObjectCategories.Where(x => x.CategoryName == categoryName).FirstOrDefault();

                if (extractedCategory != null)
                {
                    categoryIdToFind = extractedCategory.CategoryID;
                    query = query.Where(p => p.CategoryID == categoryIdToFind);
                }
            }

            return query;
        }
    }
}