<%@ Page Title="Catalog Items" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
         CodeBehind="CatalogObjectList.aspx.cs" Inherits="CatalogiaWebForms.CatalogObjectList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div>
            <hgroup>
                <h2><%: Page.Title %></h2>
            </hgroup>

            <asp:ListView ID="catalogObjectList" runat="server" 
                DataKeyNames="ObjectId" GroupItemCount="4"
                ItemType="CatalogiaWebForms.Models.CatalogObject" SelectMethod="GetCatalogItems">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td/>
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td runat="server">
                        <table>
                            <tr>
                                <td>
                                    <a href="<%#: GetRouteUrl("CatalogObjectsByNameRoute", new { categoryName = Item.ObjectName }) %>">
                                    <!--
                                    <a href="CatalogObjectDetails.aspx?ObjectId=<%#:Item.ObjectId %>">
                                    -->
                                        <img src="/Catalog/Images/Thumbs/<%#:Item.ImagePath%>"
                                            width="100" height="75" style="border: solid" />
                                    </a>
                                </td>                                
                            </tr>
                            <tr>
                                <td>
                                    <a href="<%#: GetRouteUrl("CatalogObjectsByNameRoute", new { categoryName = Item.ObjectName }) %>">                                    
                                    <a href="CatalogObjectDetails.aspx?ObjectId=<%#:Item.ObjectId%>">
                                        <span>
                                            <%#:Item.OtherName%>
                                        </span>
                                    </a>
                                    <br />
                                    <span>
                                        <b>Price: </b><%#:String.Format("{0:c}", Item.Price)%>
                                    </span>
                                    <br />
                                    <a href="/AddToCart.aspx?ObjectId=<%#Item.ObjectId %>" />
                                    <span class="CatalogListItem">
                                        <b>Order this Item</b>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        </p>
                    </td>
                </ItemTemplate>
                <LayoutTemplate>
                    <table style="width:100%;">
                        <tbody>
                            <tr>
                                <td>
                                    <table id="groupPlaceholderContainer" runat="server" style="width:100%">
                                        <tr id="groupPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>
    </section>
</asp:Content>
