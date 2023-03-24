using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Data;

namespace Sample
{
    public static class DataExtension
    {
        public static void AddSampleDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            string con = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            if (string.IsNullOrEmpty(con))
            {
                throw new ArgumentNullException("Connection can not be null");
            }
            services.AddDbContext<SampleDbContext>(options =>
            {
                options.UseMySQL(con);
            });
        }
    }
}
