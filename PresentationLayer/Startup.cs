using api_fact_weather_by_city.Mapper;
using AutoMapper;
using DBLayer.Context;
using LocationExpander;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using BL.Services;
using BL.IServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using BusinessLayer.IServices;
using DB.TableModels;
using DB.IRepository;
using Microsoft.AspNetCore.Builder;
using api_fact_weather_by_city.Controllers;
using Static.Service;
using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.DBModel;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using DatabaseLayer.IDbContext;
using api_fact_weather_by_city.Filters;
using api_fact_weather_by_city.ViewModel;
using BusinessLayer.Services;
using DatabaseLayer.Repository;
using DatabaseLayer.IRepository;
using DatabaseLayer.Context;
using api_fact_weather_by_city.JSONModels._29._131;
using api_fact_weather_by_city.Mapper.GidroMorpf.GmfProtocol;
using Microsoft.AspNetCore.CookiePolicy;
using Swashbuckle;
using Microsoft.OpenApi.Models;
using Swashbuckle.Examples;
using BL.Models;

namespace ServerRestAPI
{
    public class Startup
    {   
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration {get;}

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().AddXmlSerializerFormatters().AddControllersAsServices();
            services.AddControllersWithViews();
            services.Configure<RazorViewEngineOptions>(options => {options.ViewLocationExpanders.Add(new ViewLocationExpander());});
            services.AddMemoryCache();

            
            //экземпляры сервисов
            services.AddScoped<IGidroMorpfServices, GidroMorpfServices>();

            //services.AddScoped<ISynopServicesAsync<SynopBL>, MeteoServices<SynopBL>>();
            services.AddScoped<IGidroMorpfRepository, GidroMorpfRepository>();
            services.AddScoped<UserServices<UserBL>>();

            //filter
            services.AddScoped<ResponceFormatFilter<GmfPunctBL, GmfPunctBL>>();
            services.AddScoped<ResponceFormatFilter<GmfTotalJson, GmfTotalPL>>();
            //экземпляры баз         
            services.AddScoped<UserdbContext>();
            services.AddScoped<GidroMorpfContext>();

            services.AddSwaggerGen(option => 
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                option.EnableAnnotations();
                option.SchemaFilter<SwaggerSchemaCustomFilter>();              
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AdminRoute", builder =>
                {
                    builder.SetIsOriginAllowed(hostName => false);
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });

                options.AddDefaultPolicy(builder => 
                {
                    builder.SetIsOriginAllowed(hostName => true);
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                    
                });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
            ( options => 
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(3);
                }
            );
            services.AddOutputCache();
            
            //отдельно маппер
            var mapperConfig = new MapperConfiguration(mc =>
            {       
                mc.AddProfile(new GmfCategory_to_GmfCategoryDB());
                mc.AddProfile(new GmfCategoryDB_to_GmfCategoryBL());
                mc.AddProfile(new GmfCategoryBL_GmfCategoryPL());
                mc.AddProfile(new GmfClass3_to_GmfClass3DB());
                mc.AddProfile(new GmfClass3DB_to_GmfClass3BL());
                mc.AddProfile(new GmfClass3BL_to_GmfClass3PL());
                mc.AddProfile(new GmfClass5_to_GmfClass5DB());
                mc.AddProfile(new GmfClass5DB_to_GmfClass5BL());
                mc.AddProfile(new GmfClass5BL_to_GmfClass5PL());
                mc.AddProfile(new GmfEstamination_to_GmfEstaminationDB());
                mc.AddProfile(new GmfEstaminationDB_to_GmfEstaminationBL());
                mc.AddProfile(new GmfEstaminationBL_to_GmfEstaminationPL());
                mc.AddProfile(new GmfIndicator_to_GmfIndicatorDB());
                mc.AddProfile(new GmfIndicatorBL_to_GmfIndicatorPL());
                mc.AddProfile(new GmfIndicatorDB_to_GmfIndicatorBL());
                mc.AddProfile(new GmfProtocol_to_GmfProtocolDB());
                mc.AddProfile(new GmfProtocolDB_to_GmfProtocolBL());
                mc.AddProfile(new GmfProtocolBL_to_GmfProtocalPL());
                mc.AddProfile(new GmfPunct_to_GmfPunctDB());
                mc.AddProfile(new GmfPunctDB_to_GmfPunctBL());
                mc.AddProfile(new GmfPunctBL_to_GmfPunctPL());
                mc.AddProfile(new GmfTotal_to_GmfTotalDB());
                mc.AddProfile(new GmfTotalDB_to_GmfTotalBL());
                mc.AddProfile(new GmfTotalBL_to_GmfTotalPL());
                mc.AddProfile(new GmfZona_to_GmfZonaDB());
                mc.AddProfile(new GmfZonaDB_to_GmfZonaBL());
                mc.AddProfile(new GmfZonaBL_to_GmfZonaPL());
                mc.AddProfile(new GmfProtocolPL_to_GmfProtocolJson());
                mc.AddProfile(new GmfTotalPL_to_GmfTotalJson());
                mc.AddProfile(new GmfProtocolJson_to_GmfProtocolPL());
                mc.AddProfile(new GmfProtocolPL_to_GmfProtocolJson());
                mc.AddProfile(new User_to_UserBL());
                mc.AddProfile(new PageAccessRight_to_AccessRightBL());
                mc.AddProfile(new GmfRiver_to_GmfRiverDB());
                mc.AddProfile(new GmfRiverDB_to_GmfRiverBL());
                mc.AddProfile(new GmfRiverBL_to_GmfRiverPL());
                mc.AddProfile(new GmfRegion_to_GmfRegionBD());
                mc.AddProfile(new GmfRegionDB_to_GmfRegionBL());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            
        }

        
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
 
            app.UseRouting();

            app.UseCors(builder => 
            {
                builder.SetIsOriginAllowed(hostName => true);
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                
            });

            //app.UseCors("AdminRoute");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOutputCache();

            app.UseSwagger(option => 
            {
                option.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");    
                option.RoutePrefix = string.Empty;
            });
           
            app.UseEndpoints(endpoints =>
                {
                    //endpoints.MapHub<GorimSignalRServer>("/gorim_maps");
                    endpoints.MapControllers(); 
                }
            );

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}


