using Microsoft.EntityFrameworkCore; // UseSqlServer
using Microsoft.Extensions.DependencyInjection; // IServiceCollection
namespace SolidEdu.Shared;//namespace net 6
public static class EcommerceContextExtensions
{
    /// <summary>
    /// Adds EcommerceContext to the specified IServiceCollection. Uses the SqlServer database provider.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString">Set to override the default.</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    /// IServiceCollection tap hop chua cac services
    /// Them phuong thuc AddEcommerceContext cho IServiceCollection bang extension method
    public static IServiceCollection AddEcommerceContext(this IServiceCollection services, string connectionString =
     "Data Source=.;Initial Catalog=SolidStore;"
     + "Integrated Security=true;MultipleActiveResultsets=true;")
    {
        services.AddDbContext<SolidStoreContext>(options => options.UseSqlServer(connectionString));
        return services;
    }
}