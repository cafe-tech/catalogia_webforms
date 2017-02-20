using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CatalogiaWebForms.Models;
using CatalogiaWebForms.Logic;

namespace CatalogiaWebForms.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string catalogObjectAction = Request.QueryString["CatalogObjectAction"];
            if (catalogObjectAction == "add")
            {
                LabelAddStatus.Text = "Object added!";
            }

            if (catalogObjectAction == "remove")
            {
                LabelRemoveStatus.Text = "Object removed!";
            }
        }


        protected void btnAddCatalogItem_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;
            String path = Server.MapPath("~/Catalog/Images/");
            if (ObjectImage.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(ObjectImage.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    // Save to Images folder.
                    ObjectImage.PostedFile.SaveAs(path + ObjectImage.FileName);
                    // Save to Images/Thumbs folder.
                    ObjectImage.PostedFile.SaveAs(path + "Thumbs/" + ObjectImage.FileName);
                }
                catch (Exception ex)
                {
                    LabelAddStatus.Text = ex.Message;
                }

                // Add the item to the Catalog Table.
                AddObjects catalogObjects = new AddObjects();
                bool addSuccess = catalogObjects.AddObject(AddObjectName.Text, AddItemDescription.Text,
                    AddCatalogObjectPrice.Text, DropDownAddCategory.SelectedValue, ObjectImage.FileName);
                if (addSuccess)
                {
                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?CatalogObjectAction=add");
                }
                else
                {
                    LabelAddStatus.Text = "Unable to add new item to database.";
                }
            }
            else
            {
                LabelAddStatus.Text = "Unable to accept file type.";
            }
        }

        
        public IQueryable GetCategories()
        {
            var _db = new CatalogiaWebForms.Models.CatalogObjectContext();
            IQueryable query = _db.CatalogObjectCategories;

            List<CatalogObjectCategory> cList = new ObservableCollection<Models.CatalogObjectCategory>((IQueryable<CatalogObjectCategory>)query).ToList();

            return query;
        }        

        public IQueryable GetCatalogObjects()
        {
            var _db = new CatalogiaWebForms.Models.CatalogObjectContext();
            IQueryable query = _db.CatalogObjects;
            return query;
        }

        protected void btnRemoveCatalogItem_Click(object sender, EventArgs e)
        {
            using (var _db = new CatalogiaWebForms.Models.CatalogObjectContext())
            {
                string objectId = ddlRemoveCatalogObject.SelectedValue;
                var myItem = (from c in _db.CatalogObjects where c.ObjectId == objectId select c).FirstOrDefault();
                if (myItem != null)
                {
                    _db.CatalogObjects.Remove(myItem);
                    _db.SaveChanges();

                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?CatalogObjectAction=remove");
                }
                else
                {
                    LabelRemoveStatus.Text = "Unable to locate catalog item.";
                }
            }
        }
    }
}