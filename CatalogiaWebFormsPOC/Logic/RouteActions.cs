using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace CatalogiaWebForms.Logic
{
    public class RouteActions
    {
        internal void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "CatalogObjectsByCategoryRoute",
                "Category/{categoryName}",
                "~/CatalogObjectList.aspx"
            );
            routes.MapPageRoute(
                "CatalogObjectsByNameRoute",
                "CatalogObject/{catalogObjectName}",
                "~/CatalogObjectDetails.aspx"
            );
        }
    }
}