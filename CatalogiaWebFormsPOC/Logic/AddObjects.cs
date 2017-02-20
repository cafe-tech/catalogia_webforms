using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CatalogiaWebForms.Models;

namespace CatalogiaWebForms.Logic
{
    public class AddObjects
    {
        public bool AddObject(string objName, string objDesc, string objServicePrice, string objCategory, string objImagePath)
        {
            var newObject = new CatalogObject();
            newObject.ObjectName = objName;
            newObject.OtherName = objDesc;
            newObject.Price = Convert.ToDouble(objServicePrice);
            newObject.ImagePath = objImagePath;
            newObject.CategoryID = Convert.ToInt32(objCategory);

            using (CatalogObjectContext _db = new CatalogObjectContext())
            {
                _db.CatalogObjects.Add(newObject);
                _db.SaveChanges();
            }

            // Success.
            return true;
        }
    }
}