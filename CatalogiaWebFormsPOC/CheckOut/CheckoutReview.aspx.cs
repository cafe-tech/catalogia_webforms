using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CatalogiaWebForms.Logic;
using CatalogiaWebForms.Models;

namespace CatalogiaWebForms.CheckOut
{
    public partial class CheckOutReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NVPAPICaller payPalCaller = new NVPAPICaller();

                string retMsg = "";
                string token = "";
                string PayerID = "";
                NVPCodec decoder = new NVPCodec();
                token = Session["token"].ToString();

                bool ret = payPalCaller.GetCheckoutDetails(token, ref PayerID, ref decoder, ref retMsg);
                if (ret)
                {
                    Session["payerId"] = PayerID;

                    var myOrder = new Order();
                    myOrder.OrderDate = Convert.ToDateTime(decoder["TIMESTAMP"].ToString());
                    myOrder.Username = User.Identity.Name;
                    myOrder.FirstName = decoder["FIRSTNAME"].ToString();
                    myOrder.LastName = decoder["LASTNAME"].ToString();
                    myOrder.Address = decoder["SHIPTOSTREET"].ToString();
                    myOrder.City = decoder["SHIPTOCITY"].ToString();
                    myOrder.State = decoder["SHIPTOSTATE"].ToString();
                    myOrder.PostalCode = decoder["SHIPTOZIP"].ToString();
                    myOrder.Country = decoder["SHIPTOCOUNTRYCODE"].ToString();
                    myOrder.Email = decoder["EMAIL"].ToString();
                    myOrder.Total = Convert.ToDecimal(decoder["AMT"].ToString());

                    // Verify total payment amount as set on CheckoutStart.aspx.
                    try
                    {
                        decimal paymentAmountOnCheckout = Convert.ToDecimal(Session["payment_amt"].ToString());
                        decimal paymentAmoutFromPayPal = Convert.ToDecimal(decoder["AMT"].ToString());
                        if (paymentAmountOnCheckout != paymentAmoutFromPayPal)
                        {
                            Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.");
                    }

                    // Get DB context.
                    CatalogObjectContext _db = new CatalogObjectContext();

                    // Add order to DB.
                    _db.Orders.Add(myOrder);
                    try
                    {
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                    }

                    // Get the shopping cart items and process them.
                    using (CatalogiaWebForms.Logic.ShoppingCartActions usersShoppingCart = new CatalogiaWebForms.Logic.ShoppingCartActions())
                    {
                        List<CartItem> orders = usersShoppingCart.GetCartItems();

                        // Add OrderDetail information to the DB for each catalog item ordered.
                        for (int i = 0; i < orders.Count; i++)
                        {
                            // Create a new OrderDetail object.
                            var currentDetail = new OrderDetail();
                            currentDetail.OrderId = myOrder.OrderId;
                            currentDetail.Username = User.Identity.Name;
                            currentDetail.ObjectId = orders[i].CatalogObject.ObjectId;
                            currentDetail.Quantity = orders[i].Quantity;
                            currentDetail.UnitPrice = orders[i].CatalogObject.Price;

                            // Add OrderDetail to DB.
                            _db.OrderDetails.Add(currentDetail);
                            _db.SaveChanges();
                        }

                        // Set OrderId.
                        Session["currentOrderId"] = myOrder.OrderId;

                        // Display Order information.
                        List<Order> orderList = new List<Order>();
                        orderList.Add(myOrder);
                        ShipInfo.DataSource = orderList;
                        ShipInfo.DataBind();

                        // Display OrderDetails.
                        OrderItemList.DataSource = orders;
                        OrderItemList.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("CheckoutError.aspx?" + retMsg);
                }
            }
        }

        protected void CheckoutConfirm_Click(object sender, EventArgs e)
        {
            Session["userCheckoutCompleted"] = "true";
            Response.Redirect("~/Checkout/CheckoutComplete.aspx");
        }
    }
}