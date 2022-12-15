using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Admin.Data;

public class ProductContext : DbContext{
    public DbSet<Product> Products=>Set<Product>();

    public string DbPath{get;set;}

    public ProductContext()
    {
        var path=Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath=Path.Join(path, "Carved-rock.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        =>options.UseSqlite($"Data Source={DbPath}");

}