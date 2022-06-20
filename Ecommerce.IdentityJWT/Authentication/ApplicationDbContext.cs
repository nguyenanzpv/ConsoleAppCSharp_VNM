using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.IdentityJWT.Authentication;

public class ApplicationDbContext:IdentityDbContext<IdentityUser>// IdentityUser replace User Entity manual create to save info user
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {

    }

    //use interactive with db
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
