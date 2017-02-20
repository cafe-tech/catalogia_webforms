<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CatalogObjectDetails.aspx.cs" Inherits="CatalogiaWebForms.CatalogObjectDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="catalogObjectDetail" runat="server" ItemType="CatalogiaWebForms.Models.CatalogObject" SelectMethod="GetCatalogObjectDetails" RenderOuterTable="false">
        <ItemTemplate>
            <div>
                <h1><%#:Item.ObjectName %></h1>
            </div>
            <table>
                <tr>
                    <td>
                        <img src="/Catalog/Images/<%#:Item.ImagePath %>"  style="border:solid;height:300px" alt="<%#:Item.ObjectName %>" />
                    </td>
                    <td>&nbsp;</td>
                    <td style="vertical-align:top; text-align:left;">
                        <b>Description:</b><br /><%#:Item.OtherName %>
                        <br />
                        <span><b>Price: </b>&nbsp;<%#:String.Format("{0:c}", Item.Price) %></span>
                        <br />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
