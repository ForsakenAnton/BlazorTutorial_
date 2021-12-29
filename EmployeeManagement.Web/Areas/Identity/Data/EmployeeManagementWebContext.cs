using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeeManagement.Web.Data;

public class EmployeeManagementWebContext : IdentityDbContext<IdentityUser>
{
    public EmployeeManagementWebContext(DbContextOptions<EmployeeManagementWebContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

public class BloggingContextFactory : IDesignTimeDbContextFactory<EmployeeManagementWebContext>
{
    public EmployeeManagementWebContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmployeeManagementWebContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EmployeeManagement.Web;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new EmployeeManagementWebContext(optionsBuilder.Options);
    }
}
