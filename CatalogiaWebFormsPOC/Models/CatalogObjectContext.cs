
using System.Data.Entity;


namespace CatalogiaWebForms.Models
{
    public class CatalogObjectContext : DbContext
    {
        public CatalogObjectContext() : base("CatalogiaWebForms")
        {
        }

        public DbSet<CatalogObject> CatalogObjects { get; set; }
        public DbSet<CatalogObjectCategory> CatalogObjectCategories { get; set; }
        public DbSet<CatalogCustomField> CatalogCustomFields { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}