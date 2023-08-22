using ReportApp.API.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace ReportApp.API
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "BethanysPieShopHRM"));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHttpClient();

            services.AddScoped<IUserModel, UserModel>();

            services.AddScoped<IBugModel, BugModel>();

            services.AddScoped<IFeedbackModel, FeedbackModel>();
            services.AddScoped<IAttachment, AttachmentModel>();


            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7192")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });



            services.AddControllers();
            //.AddJsonOptions(options => options.JsonSerializerOptions.ca);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigins");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
