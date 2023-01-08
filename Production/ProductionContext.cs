using Microsoft.EntityFrameworkCore;
using Production.Models;

namespace Production
{
    public class ProductionContext : DbContext
    {
        public ProductionContext(DbContextOptions<ProductionContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Card>(entity => {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            }) ;
        }

        public DbSet<Executor> Executors { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<CardStatus> CardStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CardOperation> CardOperations { get; set; }
        public DbSet<CardMaterial> CardMaterials { get; set; }
        public DbSet<CardOwnProduct> CardOwnProducts { get; set; }
        public DbSet<CardPurchasedProduct> CardPurchasedProducts { get; set; }
        public DbSet<CardOwnProductOperation> CardOwnProductsOperations { get; set; }
        public DbSet<RepairType> RepairTypes { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<UnlockedPeriod> UnlockedPeriods { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestToCreateStagesIn1S> RequestsToCreateStagesIn1S { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateOperation> TemplateOperations { get; set; }
        public DbSet<CardOwnProductRepairOperation> CardOwnProductRepairOperations { get; set; }
        public DbSet<PermissionCard> PermissionCards { get; set; }
        public DbSet<PermissionCardOperation> PermissionCardOperations { get; set; }
        public DbSet<PermissionCardMaterial> PermissionCardMaterials { get; set; }
        public DbSet<PermissionCardProduct> PermissionCardProducts { get; set; }
        public DbSet<PermissionCardPurchasedProduct> PermissionCardPurchasedProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductRelation> ProductRelations { get; set; }

        public Task<List<Order>> GetOrdersByProductAsync(string productCode)
        {
            return Orders.FromSqlRaw("CROrders @ProductCode = @p0", 
                parameters: new[] { productCode })
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<ProductRelation>> GetProductRelationsAsync(string code, int count)
        {
            return ProductRelations.FromSqlRaw("CRGetProductRelations @Code = @p0, @Count = @p1", 
                parameters: new[] { code, count.ToString() })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
