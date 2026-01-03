using Microsoft.EntityFrameworkCore;
using WMTDotNetTraning.ConsoleApp.Models;

namespace WMTDotNetTraning.ConsoleApp;

public class AppDbContext: DbContext
{
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=Temporary123; TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
    
    public DbSet<BlogDataModel>  Blogs { get; set; }
}