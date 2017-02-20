<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="CatalogiaWebForms.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CatalogServiceOrders" runat="server" class="ContentHead"><h1>Catalog Orders</h1></div>
    <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="False" ShowFooter="True"
            Gridlines="Vertical" CellPadding="4" 
            ItemType="CatalogiaWebForms.Models.CartItem" SelectMethod="GetShoppingCartItems"
            CssClass="table table-stripped table-bordered" >
        <Columns>
            <asp:BoundField DataField="CatalogObject.ObjectId" HeaderText="Item ID" SortExpression="CatalogObject.ObjectId" />
            <asp:BoundField DataField="CatalogObject.ObjectName" HeaderText="Description" />
            <asp:BoundField DataField="CatalogObject.Price" HeaderText="Price (per service)" DataFormatString="{0:c}" />
            <asp:TemplateField HeaderText="Qty">
                <ItemTemplate>
                    <asp:TextBox ID="txtServiceQty" Width="40" runat="server" Text="<%#:Item.Quantity %>" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Total">
                <ItemTemplate>
                    <%#: String.Format("{0:c}", ((Convert.ToDouble(Item.Quantity)) * Convert.ToDouble(Item.CatalogObject.Price)) ) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remove Item">
                <ItemTemplate>
                    <asp:CheckBox id="RemoveItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div>
        <p></p>
        <strong>
            <asp:Label ID="lblTotal" runat="server" Text="Service Order Total: " ></asp:Label>
            <asp:Label ID="lblTotalText" runat="server" EnableViewState="false"></asp:Label>
        </strong>
    </div>
    <br />
       <table> 
    <tr>
      <td>
        <asp:Button ID="UpdateBtn" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
      </td>
      <td>
        <asp:ImageButton ID="CheckoutImageBtn" runat="server" 
                      ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" 
                      Width="145" AlternateText="Check out with PayPal" 
                      OnClick="CheckoutBtn_Click" 
                      BackColor="Transparent" BorderWidth="0" />

      </td>
    </tr>
    </table>
</asp:Content>
