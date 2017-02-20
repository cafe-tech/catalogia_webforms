using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CatalogiaWebForms.Models;
using CatalogiaWebForms.Logic;

using System.Collections.Specialized;
using System.Collections;
using System.Web.ModelBinding;

namespace CatalogiaWebForms
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ShoppingCartActions usc = new Logic.ShoppingCartActions())
            {
                decimal cartTotal = 0;
                cartTotal = usc.GetTotal();
                if (cartTotal > 0)
                {
                    // Display Total.
                    lblTotalText.Text = string.Format("{0:c}", cartTotal);
                }
                else
                {
                    lblTotalText.Text = "";
                    lblTotal.Text = "";
                    CatalogServiceOrders.InnerText = "Currently no orders";
                    UpdateBtn.Visible = false;
                    CheckoutImageBtn.Visible = false;
                }
            }
        }

        public List<CartItem> GetShoppingCartItems()
        {
            ShoppingCartActions sca = new ShoppingCartActions();
            return sca.GetCartItems();
        }

        public List<CartItem> UpdateCartItems()
        {
            using (ShoppingCartActions usc = new Logic.ShoppingCartActions())
            {
                String cartId = usc.GetCartId();

                ShoppingCartActions.ShoppingCartUpdates[] cartUpdates = new ShoppingCartActions.ShoppingCartUpdates[CartList.Rows.Count];
                for (int i = 0; i < CartList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CartList.Rows[i]);
                    cartUpdates[i].ObjectId = rowValues["CatalogObject.ObjectId"].ToString();

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)CartList.Rows[i].FindControl("RemoveItem");
                    cartUpdates[i].RemoveItem = cbRemove.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)CartList.Rows[i].FindControl("txtServiceQty");
                    cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                }

                usc.UpdateShoppingCartDatabase(cartId, cartUpdates);
                CartList.DataBind();
                lblTotalText.Text = string.Format("{0:c}", usc.GetTotal());
                return usc.GetCartItems();
            }
        }

        public static IOrderedDictionary GetValues(GridViewRow iRow)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in iRow.Cells)
            {
                if (cell.Visible)
                {
                    cell.ContainingField.ExtractValuesFromCell(values, cell, iRow.RowState, true);
                }                
            }
            return values;
        }

        protected void UpdateBtn_Click (object sender, EventArgs e)
        {
            UpdateCartItems();
        }

        protected void CheckoutBtn_Click(object sender, EventArgs e)
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                Session["payment_amt"] = usersShoppingCart.GetTotal();
            }
            Response.Redirect("Checkout/Checkout.aspx");
        }

    }
}