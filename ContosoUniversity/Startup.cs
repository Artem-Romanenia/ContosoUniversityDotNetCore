using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniversity.Data;
using ContosoUniversity.Infrastructure;
using ContosoUniversity.Infrastructure.Tags;
using FluentValidation.AspNetCore;
using HtmlTags;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MediatR.Extensions.Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Api;
using OdeToCode.AddFeatureFolders;

namespace ContosoUniversity
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
            services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper();

            services.AddMediatR();

            services.AddHtmlTags(new TagConventions());

            services.AddMvc(opt =>
                {
                    opt.Filters.Add(typeof(DbContextTransactionFilter));
                    opt.Filters.Add(typeof(ValidatorActionFilter));
                    opt.ModelBinderProviders.Insert(0, new EntityModelBinderProvider());
                    opt.AddMediatrMvcConvention(new GenericConvention());
                })
                .AddFeatureFolders(new FeatureFolderOptions() { DeriveFeatureFolderName = GenericConvention.DeriveFeatureFolderName })
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .AddMediatrMvcGenericController(new GenericControllerFeatureProvider(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                Mapper.AssertConfigurationIsValid();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
