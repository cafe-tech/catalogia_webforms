using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CatalogiaWebForms.Models
{
    public class CatalogObjectInitializer : DropCreateDatabaseAlways<CatalogObjectContext>
    {
        protected override void Seed(CatalogObjectContext context)
        {            
            GetCatalogObjects().ForEach(c => context.CatalogObjects.Add(c));
            GetCustomCatalogFields().ForEach(cu => context.CatalogCustomFields.Add(cu));
            GetCategories().ForEach(coc => context.CatalogObjectCategories.Add(coc));
        }

        private static List<CatalogObject> GetCatalogObjects()
        {
            var catalogObjects = new List<CatalogObject> {
                new CatalogObject
                {
                    ObjectId = "AE213C15-6E71-44DB-9ACD-00D1569242EF",
                    ObjectName = "Cars",
                    OtherName = "Carz",
                    ImagePath ="",                                        
                    Price = 100.0 ,
                    CategoryID = 1
                },
                new CatalogObject
                {
                    ObjectId = "36020516-D4DB-4756-88F6-23158C4DE515",
                    ObjectName = "Planes",
                    OtherName = "Planez",
                    ImagePath ="",                    
                    Price = 250.00,
                    CategoryID = 2
                },
            };

            return catalogObjects;
        }

        private static List<CatalogCustomField> GetCustomCatalogFields()
        {
            var customCatalogFields = new List<CatalogCustomField> {
                new CatalogCustomField
                {
                    CustomFieldId = "1",
                    ObjectID= "1",
                    Collector  ="John Good",
                },
                new CatalogCustomField
                {
                    CustomFieldId = "2",
                    ObjectID= "2",
                    Collector  ="John Bad"
                },
            };

            return customCatalogFields;
        }

        private static List<CatalogObjectCategory> GetCategories()
        {
            var categories = new List<CatalogObjectCategory> {
                new CatalogObjectCategory
                {
                    CategoryID = 1,
                    CategoryName = "Pottery",
                     ObjectDescription = "Pots, pans, plates"
                },
                new CatalogObjectCategory
                {
                    CategoryID = 2,
                    CategoryName = "Relics",
                    ObjectDescription = "Early 1000 AD artifacts"
                },
                new CatalogObjectCategory
                {
                    CategoryID = 3,
                    CategoryName = "Art Pieces",
                    ObjectDescription = "Paintings, sculptures, of all types"
                },
            };

            return categories;
        }
    }
}