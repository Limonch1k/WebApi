using api_fact_weather_by_city.Mapper;
using AutoMapper;
using DBLayer.Context;
using GeneralObject.Constants;
using LocationExpander;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using BL.Services;
using BL.IServices;
using BL.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using BusinessLayer.IServices;
using DB.TableModels;
using DB.IRepository;
using DB.DBModels;
using DB.Repository;
using Microsoft.AspNetCore.Builder;
using api_fact_weather_by_city.Controllers;
using Static.Service;
using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.DBModel;
using BusinessLayer.Models;
using api_fact_weather_by_city.Mapper.ParamModels;
using Microsoft.EntityFrameworkCore;
using DatabaseLayer.IDbContext;
using api_fact_weather_by_city.Filters;
using PL.PLModels;
using api_fact_weather_by_city.ViewModel;
using api_fact_weather_by_city.XMLModels;
using BusinessLayer.Services;
using DatabaseLayer.Repository;
using DatabaseLayer.IRepository;
using DatabaseLayer.Context;
using api_fact_weather_by_city.JSONModels._29._131;
using api_fact_weather_by_city.Mapper.GidroMorpf.GmfProtocol;

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
            services.AddCors(opt => 
            {
                opt.AddPolicy(name: "origins",policy => policy.WithOrigins("http://192.168.211.210", 
                    "http://192.168.211.64",
                    "http://192.168.211.60:3000",
                    "http://localhost:3000"
                    ));
            });

            services.AddMvc().AddXmlSerializerFormatters().AddControllersAsServices();
            services.AddControllersWithViews();
            services.Configure<RazorViewEngineOptions>(options => {options.ViewLocationExpanders.Add(new ViewLocationExpander());});
            services.AddMemoryCache();

            
            //экземпляры сервисов
            services.AddTransient<INoDataServices<SynopBL, NullDataTable>, NullDataServices<SynopBL, NullDataTable>>();
            services.AddScoped<NullDataServices<SynopBL, NullDataTable>>();
            services.AddScoped<IGidroMorpfServices, GidroMorpfServices>();

            services.AddTransient<INoDataServices<MeasuringAMS_BL, NullDataTable>, NullDataServices<MeasuringAMS_BL, NullDataTable>>();
            services.AddTransient<INoDataServices<GroundData_BL, NullDataTable>, NullDataServices<GroundData_BL, NullDataTable>>();

            services.AddScoped<NullDataServices<MeasuringAMS_BL, NullDataTable>>();
            services.AddScoped<NullDataServices<GroundData_BL, NullDataTable>>();

            services.AddScoped<ISynopServicesAsync<AverageTempBL>, TempServices<AverageTempBL>>();  
            services.AddTransient<IUserServices<UserBL>, UserServices<UserBL>>();
            services.AddScoped<UserServices<UserBL>>();


            services.AddScoped<ISynopServicesAsync<SynopBL>, MeteoServices<SynopBL,SynopDB>>();
            services.AddScoped<ISynopServicesAsync<MeasuringAMS_BL>, MeteoServices<MeasuringAMS_BL,MeasuringAmsDB>>();
            services.AddScoped<ISynopServicesAsync<GroundData_BL>, MeteoServices<GroundData_BL, GroundData_DB>>();

            services.AddScoped<MeteoServices<SynopBL,SynopDB>>();
            services.AddScoped<MeteoServices<MeasuringAMS_BL,MeasuringAmsDB>>();
            services.AddScoped<MeteoServices<GroundData_BL, GroundData_DB>>();


            //services.AddScoped<ISynopServicesAsync<SynopBL>, MeteoServices<SynopBL>>();
            services.AddScoped<IResorceRepositoryAsync<SynopDB>, UniversalMeteoRepository<SynopDB,Synop>>();
            services.AddScoped<IResorceRepositoryAsync<MeasuringAmsDB>, UniversalMeteoRepository<MeasuringAmsDB,MeasuringAm>>();
            services.AddScoped<IResorceRepositoryAsync<GroundData_DB>, UniversalMeteoRepository<GroundData_DB, GroundDatum>>();

            services.AddScoped<IDbContext<DbSet<Synop>>, MeteoContext>();
            services.AddScoped<IDbContext<DbSet<MeasuringAm>>, MeteoContext>();
            services.AddScoped<IDbContext<DbSet<GroundDatum>>, NWPContext>();
            services.AddScoped<IGidroMorpfRepository, GidroMorpfRepository>();

            //filter
            services.AddScoped<ResponceFormatFilter<SynopPL,SynopXML>>();
            services.AddScoped<ResponceFormatFilter<MeasuringAMS_PL, MeasuringAMS_PL>>();
            services.AddScoped<ResponceFormatFilter<GroundData_PL, GroundDataXML>>();
            //11111111111111111111111111111111111111111111
            //111111111111111111111111111111
            services.AddScoped<ResponceFormatFilter<GmfPunctBL, GmfPunctBL>>();
            //111111111111111111111111111111
            //11111111111111111111111111111
            services.AddScoped<InputModelFilter<GmfProtocolJson, GmfProtocolPL>>();

            //экземпляры баз
            services.AddScoped<CligtsContext>();          
            services.AddSingleton<UserdbContext>();
            services.AddScoped<MeteoContext>();
            services.AddScoped<NWPContext>();
            services.AddScoped<GidroMorpfContext>();
            
            //не помню что
            services.AddSingleton<ColumnsName>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddOutputCache();
            
            //отдельно маппер
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataRow_to_AverageTempDB());
                mc.AddProfile(new AverageTempDB_to_AverageTempBL());
                mc.AddProfile(new AverageTempBL_to_AverageTempPL());
                mc.AddProfile(new Synop_to_SynopDB()); 
                mc.AddProfile(new SynopDB_to_SynopBL());
                mc.AddProfile(new SynopBL_to_SynopPL());
                mc.AddProfile(new SynopPL_to_SynopXML());
                mc.AddProfile(new MeasuringAMS_to_MeasuringAMS_DB());
                mc.AddProfile(new MeasuringAMS_DB_to_MeasuringAMS_BL());
                mc.AddProfile(new MeasuringAMS_BL_to_MeasuringAMS_PL());
                mc.AddProfile(new MeasuringAMS_PL_to_MeasuringAMS_XML());
                mc.AddProfile(new MeteoParamModel_BL_to_MeteoParamModel_DB());
                mc.AddProfile(new GroundData_to_GroundData_DB());
                mc.AddProfile(new GroundData_DB_to_GroundData_BL());
                mc.AddProfile(new GroundData_BL_to_GroundData_PL());
                mc.AddProfile(new GroundData_PL_to_GroundData_XML());
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
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOutputCache();
           
            app.UseEndpoints(endpoints =>
                {
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


