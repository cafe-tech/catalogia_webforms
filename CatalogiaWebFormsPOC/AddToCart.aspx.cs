using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CatalogiaWebForms.Logic;

namespace CatalogiaWebForms
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string inCatalogObjectId = Request.QueryString["ObjectId"];

            if ( !String.IsNullOrEmpty( inCatalogObjectId ) )
            {
                ShoppingCartActions shoppingCart = new Logic.ShoppingCartActions();                
                shoppingCart.AddToCart(inCatalogObjectId);                
            }
            else
            {
                throw new Exception("No Catalog Item Received.");                    
            }

            Response.Redirect("ShoppingCart.aspx");
        }
    }
}